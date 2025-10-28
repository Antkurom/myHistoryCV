#include <fstream>
#include <iostream>
#include <string>

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
    cout << "Number of comparisons: " << comparisons << endl;
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
    
    generateTestData(arrayOfStrings, numbers, strings, size/2);
    selectionSort(numbers, size/2, strings);
    cleanupArrays(numbers, strings);


    generateTestData(arrayOfStrings, numbers, strings, size);
    selectionSort(numbers, size, strings);
    cleanupArrays(numbers, strings);

    return 0;
}
