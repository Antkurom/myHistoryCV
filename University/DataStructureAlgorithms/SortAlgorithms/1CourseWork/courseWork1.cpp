#include <fstream>
#include <iostream>
#include <string>

using namespace std;

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
    int* array1 = new int[size / 2];
    ifstream file("quotes.data");
    string line;
    if (file.is_open()) {
        int i = 0;
        while (getline(file, line)) {
            if (i < size/2) {
                array1[i] = ;
                i++;
            } else {
                break;
        }
        file.close();
    } else {
        cerr << "Unable to open file!" << endl;
    }
    cout << size << endl;
    return 0;
}
