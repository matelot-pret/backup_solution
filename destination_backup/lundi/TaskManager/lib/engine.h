#ifndef ENGINE_H
#define ENGINE_H

#include <time.h>
#include <stdio.h>
#include <stdlib.h>

enum State { EN_COURS = 0, TERMINE = 1};

typedef struct {
	unsigned short jour;
	unsigned short month;
	unsigned short year;
} Date;

typedef struct{
	unsigned long id;
	char title[500];
	enum State state_of_task;
	Date creation_date;
	Date done_date;
} Task;

#endif