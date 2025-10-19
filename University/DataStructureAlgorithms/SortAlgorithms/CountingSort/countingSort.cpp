#include <iostream>

int main()
{
    int array[] = { 2, 5, 3, 0, 2, 3, 0, 3 };
    int size = 8;
    int min = array[0];
    int max = array[0];
    int comparisons = 0;

    // Find max and min values first
    for (int i = 1; i < size; i++) {
        comparisons += 2;
        if (min > array[i]) {
            min = array[i];
        }
        else if (max < array[i]) {
            max = array[i];
            comparisons ++;
        }
    }

    // Creating range array containing all elements from min to max
    int range_array_size = max - min + 1;
    int* range_array = new int[range_array_size];
    for (int i = 0; i < range_array_size; i++) {
        comparisons ++;
        range_array[i] = min + i;
    }

    // Creating counting array, that contains nuber of values in initial array    
    int* count_array = new int[range_array_size];
    for (int i = 0; i < range_array_size; i++) {
        count_array[i] = 0;
        comparisons ++;
        for (int j = 0; j < size; j++) {
            comparisons +2;
            if (range_array[i] == array[j]) {
                count_array[i]++;
            }
        }
    }

    // Creating index array for calculating future index
    int* index_array = new int[range_array_size];
    index_array[0] = count_array[0];
    for (int i = 1; i < range_array_size; i++) {
        comparisons ++;
        index_array[i] = index_array[i - 1] + count_array[i];
    }

    // Creating sorted array using all that we have
    int* sorted_array = new int[size];
    for (int i = size - 1; i >= 0; i--) {
        comparisons ++;
        int place;
        for (int j = 0; j < range_array_size; j++) {
            comparisons += 2;
            if (array[i] == range_array[j]) {
                place = j;
            }
        }
        index_array[place]--;
        sorted_array[index_array[place]] = array[i];
    }

    for (int i = 0; i < size; i++)
        std::cout << sorted_array[i] << " ";
    std::cout << std::endl << "Number of comparisons: " << comparisons << std::endl;
    return 0;
}

