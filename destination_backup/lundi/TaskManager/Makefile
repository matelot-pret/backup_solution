CC = gcc 
EXEC = app
TEST_EXEC = test

CFLAGS = -Wall -Wextra -g -std=c99
LDFLAGS = -lcunit

SRC_DIR = src/
LIB_DIR = lib/

PLAY_SRC = $(SRC_DIR)app.c 
TEST_SRC = $(SRC_DIR)test.c
LIB_SRC = $(wildcard $(LIB_DIR)*.c)

PLAY_OBJ = $(PLAY_SRC:.c=.o) $(LIB_SRC:.c=.o)
TEST_OBJ = $(TEST_SRC:.c=.o) $(LIB_SRC:.c=.o)
INCLUDES = -I$(LIB_DIR)




all: $(EXEC) $(TEST_EXEC)

$(EXEC): $(PLAY_OBJ)
	$(CC) -o $@ $^ $(CFLAGS) 

$(TEST_EXEC): $(TEST_OBJ)
	$(CC) -o $@ $^ $(CFLAGS) $(LDFLAGS)

%.o : %.c
	$(CC) -o $@ -c $< $(CFLAGS) $(INCLUDES)

.PHONY: clean mrpropre doc doc_gui valgrind run

clean : 
	rm -rf $(PLAY_OBJ) $(TEST_OBJ)

mrpropre : clean
	rm -rf $(EXEC)

run : $(EXEC)
	./$(EXEC)

doc :
	doxygen doc/Doxyfile

doc_gui :
	doxywizard &

valgrind : $(EXEC)
	valgrind ./$(EXEC)