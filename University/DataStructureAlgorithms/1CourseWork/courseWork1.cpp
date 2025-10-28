#include <fstream>
#include <iostream>
#include <string>
#include <functional>

using namespace std;

void generateTestData(string arrayOfStrings[], int*& intArr, string*& strArr, int size) {
    intArr = new int[size];
    strArr = new string[size];
    for(int i = 0; i < size; i++) {
        int len = arrayOfStrings[i].size();
        strArr[i] = arrayOfStrings[i];
        if(i == 0){
            intArr[0] = len-4;
        }else{
            intArr[i] = len - 1;
        }
    }
}

void cleanupArrays(int* intArr, string* strArr) {
    delete[] intArr;
    delete[] strArr;
}

void selectionSort(int array[], int size, string realArray[]){
    int begining = 0;
    int comparisons = 0;
    while(begining != size){
        comparisons ++;
        int min = begining;
        for (int i = begining+1; i<size; i++){
            comparisons += 2;
            if(array[min] >= array[i]){
                min = i;
            }
        }
        // swap
        int temp = array[min];
        array[min] = array[begining];
        array[begining] = temp;
        string temps = realArray[min];
        realArray[min] = realArray[begining];
        realArray[begining] = temps;
        begining ++;
    }
    cout << endl << "Selection sort resulted number of comparisons with " << size << " size is: " << comparisons << endl;
}

void halfsorting(int array[], int size, string realArray[], int & changes, int & comparisons, int sp){
    for(int i = sp; (i+1)<size; i+=2){
        comparisons += 2;
        if(array[i] > array[i+1]){
            int temp = array[i];
            array[i] = array[i+1];
            array[i+1] = temp;
            string temps = realArray[i];
            realArray[i] = realArray[i+1];
            realArray[i+1] = temps;
            changes++;
        }
    }
}

void brickSort(int array[], int size, string realArray[]){
	int changes = 1;
    int comparisons = 0;
	while (changes != 0){
        changes = 0;
		// Odd phase
        halfsorting(array, size, realArray, changes, comparisons, 1);
        // Even phase
        halfsorting(array, size, realArray, changes, comparisons, 0);
	}
    cout << endl << "Brick sort resulted number of comparisons with " << size << " size is: " << comparisons << endl;
}

void countingSort(int array[], int size, string realArray[]){
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
    string* sorted_realArray = new string[size];
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
        sorted_realArray[index_array[place]] = realArray[i];
    }

    cout << endl << "Counting sort resulted number of comparisons with " << size << " size is: " << comparisons << endl;

    // Transfering sorted arrays into initial to be able to prove that all works

    for (int i = 0; i < size; i++){
        array[i] = sorted_array[i];
        realArray[i] = sorted_realArray[i];
    }
}

void displayArraysIfYouWant(int array[], int size, string realArray[]){
    int answer;
    cout << "Type what you what to see: Print numbers - 1, Print original array - 2, Print both - 3: ";
    cin >> answer;
    if (answer == 1 || answer == 3){
        for(int i = 0; i < size; i ++)
            cout << array[i] << " ";
        cout << endl;
    } 
    if (answer == 2 || answer == 3){
        for(int i = 0; i < size; i ++)
            cout << realArray[i] << endl;
    }
}

int main(){
    int size = 0;
    ifstream file("quotes.data");
    string line;
    if (file.is_open()) {
        while (getline(file, line)) {
            size ++;
        }
        file.close();
    } else {
        cerr << "Unable to open file!" << endl;
    }

    string* arrayOfStrings = new string[size];
    ifstream file1("quotes.data");
    if (file1.is_open()) {
        int i = 0;
        while (getline(file1, line)) {
            if (i < size) {
                arrayOfStrings[i] = line;
                i++;
            } else {
                break;
            }
        }
        file1.close();
    } else {
        cerr << "Unable to open file!" << endl;
    }
    
    int* numbers = nullptr;
    string* strings = nullptr;
    int sizes[] = {size/3, size/3*2, size};
    function<void(int [], int, string [])> sortingFunctions[] = {selectionSort, brickSort, countingSort};

    for(int size : sizes){
        for(auto sortingFunction : sortingFunctions){
            generateTestData(arrayOfStrings, numbers, strings, size);
            sortingFunction(numbers, size, strings);
            displayArraysIfYouWant(numbers, size, strings);
            cleanupArrays(numbers, strings);
        }
    }
    return 0;
}
