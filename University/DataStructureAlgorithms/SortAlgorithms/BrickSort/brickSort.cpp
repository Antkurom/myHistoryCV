#include <iostream>
using namespace std;

void halfsorting(int array[], int size, int & changes, int & comparisons, int sp){
    for(int i = sp; (i+1)<size; i+=2){
        comparisons += 2;
        if(array[i] > array[i+1]){
            int temp = array[i];
            array[i] = array[i+1];
            array[i+1] = temp;
            changes++;
        }
    }
}


int main (){
    int array[] = { 2, 5, 3, 0, 2, 3, 0, 3 };
    int size = 8;
	int changes = 1;
    int comparisons = 0;
	while (changes != 0){
        changes = 0;
		// Odd phase
        halfsorting(array, size, changes, comparisons, 1);
        // Even phase
        halfsorting(array, size, changes, comparisons, 0);
	}
    for(int i = 0; i < size; i++)
        cout << array[i] << " ";
    cout << endl << "Number of comparisons: " << comparisons << endl;
}
