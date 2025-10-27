#include <iostream>

int main() {
    int array[] = { 2, 5, 3, 0, 2, 3, 0, 3 };
    int size = 8;
    int beggining = 0;
    int comparisons = 0;
    while(beggining != size){
        comparisons ++;
        int min = beggining;
        for (int i = beggining+1; i<size; i++){
            comparisons += 2;
            if(array[min] >= array[i]){
                min = i;
            }
        }
        // swap
        int temp = array[min];
        array[min] = array[beggining];
        array[beggining] = temp;
        beggining ++;
    }
    for(int i = 0; i < size; i++)
        std::cout << array[i] << " ";
    std::cout << std::endl << "Number of comparisons: " << comparisons << std::endl;
    return 0;
}

