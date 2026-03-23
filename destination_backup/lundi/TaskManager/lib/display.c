#include "display.h"

/**
 * \fn display_title()
 * \brief : display the title of the app
 * \note : should only be used once and at the beginning
 */
void display_title(){
	printf("\t\t\t\t==================================================\n");
	printf("\t\t\t\t=                                                =\n");
	printf("\t\t\t\t=                  TASK MASTER                   =\n");
	printf("\t\t\t\t=                                                =\n");
	printf("\t\t\t\t==================================================\n");
}

/** 
 * \fn display_menu()
 * \brief : display the menu of the game 
 * \note : should only be used once 
 */
void display_menu(){
	printf("1- Créer une tâche \n\n");
	printf("2- Afficher les tâches terminées\n\n");
	printf("3- Afficher les tâches en cours\n\n");
	printf("4- Afficher toutes les tâches\n\n");
	printf("0- Quitter l'application\n\n");
}