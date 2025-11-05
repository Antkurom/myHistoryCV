// Importing libraries
#include <fstream>
#include <iostream>
#include <string>
#include <functional>

using namespace std;

struct Stack{
    private :
        int size;
        string* info;
        int s = -1;
    
    public :
        Stack (int sizeOfStack){
            size = sizeOfStack;
            info = new string[size];
        }
        void display(){
            cout << "Your stack: " << endl;
            for(int i = 0; i<=s; i++){
                cout << info[i] << endl;
        }
        bool search(string elem){
            int top = s;
            while (top >= 0 && info[top] != elem)
                top--;
            if (info[top] == elem)
                return True;
            else
                return False;
        }
        void addEnd(const string &val){
            if (s<size) {
                s++;
                info[s] = val;
            }
            else
                cout << "Stack overflow!" << endl;
        }
        void addStart(const string &val){
            if (s<size) {
                s++;
                for(int i = s; i>0; i--;)
                    info[i] = info[i-1];
                info[i] = val;
            }
            else
                cout << "Stack overflow!" << endl;
        }
        void addAfter(const string &val){
            if (find(info.begin(), info.end(), val) != info.end()){
                if
}

int main(){
    int size = 0;
    ifstream file("quotes.data");
    string line;
    // Calculating size of the file
	if (file.is_open()) {
        while (getline(file, line)) {
            size ++;
        }
        file.close();
    } else {
        cerr << "Unable to open file!" << endl;
    }

	// Filling the initial array
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
    return 0;
}
