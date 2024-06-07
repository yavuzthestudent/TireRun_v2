#include <stdio.h>
#include <stdlib.h>

struct node {
    int data;
    struct node *next;
};

struct node *head = NULL;
struct node *temp = NULL;
struct node *last = NULL;

void addHead(int sayi) {
    struct node *willbeadded = (struct node*)malloc(sizeof(struct node));
    willbeadded->data = sayi;
    if (head == NULL) {
        head = willbeadded;
        head->next = head;
    } else {
        willbeadded->next = head;
        last->next = willbeadded;
        head = willbeadded;
    }
}

void delFromHead() {
    if (head == NULL) {
        printf("Listede silinecek eleman yok.\n");
        return;
    } else {
        temp = head;
        if (head->next == head) { // Tek eleman varsa
            free(head);
            head = NULL;
        } else {
            while (temp->next != head) {
                temp = temp->next;
            }
            last = temp;
            temp = head->next;
            free(head);
            last->next = temp;
            head = temp;
        }
    }
}

void addFromFoot(int sayi) {
    struct node *willbeadded = (struct node*)malloc(sizeof(struct node));
    willbeadded->data = sayi;
    if (head == NULL) {
        head = willbeadded;
        head->next = head;
    } else {
        last = head;
        while (last->next != head) {
            last = last->next;
        }
        last->next = willbeadded;
        willbeadded->next = head;
    }
}

void delFromFoot() {
    if (head == NULL) {
        printf("Listede silinecek eleman yok.\n");
        return;
    } else {
        temp = head;
        if (head->next == head) { // Tek eleman varsa
            free(head);
            head = NULL;
        } else {
            while (temp->next->next != head) {
                temp = temp->next;
            }
            last = temp;
            free(temp->next);
            last->next = head;
        }
    }
}

void listele() {
    system("cls");
    if (head == NULL) {
        printf("liste boş\n");
    } else {
        temp = head;
        while (temp->next != head) {
            printf("%d ->", temp->data);
            temp = temp->next;
        }
        printf("%d", temp->data);
    }
}

int main() {
    while (1) {
        int secim, sayi;
        printf("\n 1-Liste basina eleman ekle");
        printf("\n 2-Liste sonuna eleman ekle");
        printf("\n 3-Liste basindan eleman sil");
        printf("\n 4-Liste sonundan eleman sil");
        printf("\n 5-Listeyi yazdir \n");
        scanf("%d", &secim);

        switch (secim) {
            case 1:
                printf("\n Liste basina eklenecek sayiyi gir:");
                scanf("%d", &sayi);
                addHead(sayi);
                break;

            case 2:
                printf("\n Liste sonuna eklenecek sayiyi gir: ");
                scanf("%d", &sayi);
                addFromFoot(sayi);
                break;

            case 3:
                delFromHead();
                break;

            case 4:
                delFromFoot();
                break;

            case 5:
                listele();
                break;

            default:
                printf("\n Geçersiz secim. Lütfen tekrar deneyin.\n");
                break;
        }
    }
    return 0;
}
