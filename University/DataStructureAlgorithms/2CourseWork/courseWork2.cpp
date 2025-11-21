// Importing libraries
#include <fstream>
#include <iostream>
#include <string>
#include <functional>
#include <chrono>
#include <ctime>
#include <algorithm>
#include <cctype>

using namespace std;
using namespace chrono;


class Stack{
    private :
        int Size;
        string* Info;
        int S = -1;
    
    public :
        Stack (int sizeOfStack){
            Size = sizeOfStack;
            Info = new string[Size];
        }
        // Basic functions that stack should have, everything else can use only these operations
        bool isEmpty(){
            return S==-1;
        }
        void push(const string &val){
            if (S<Size) {
                S++;
                Info[S] = val;
            }
            else
                cout << "Stack overflow!" << endl;
        }
        void pop(){
            if (!(isEmpty())){
                S--;
            } else {
                cout << "Stack is empty!" << endl;
            }
        }
        string peek(){
            return Info[S];
        }
        int size(){
            return S+1;
        }

        // New functionality using basic functions
        void display(){
            int tSize = size();
            Stack tStack(tSize);
            cout << "Stack elements from top to bottom:"<< endl;
            while(!(isEmpty())){
                string top = peek();
                cout << top << endl;
                tStack.push(top);
                pop();
            }
            cout << endl;
            while(!(tStack.isEmpty())){
                string top = tStack.peek();
                push(top);
                tStack.pop();
            }
        }

        bool search(const string &val){
            int tSize = size();
            Stack tStack(tSize);
            bool finded = false;
            while(!(isEmpty())){
                string top = peek();
                if (top == val){
                    finded = true;
                    break;
                }
                tStack.push(top);
                pop();
            }
            while(!(tStack.isEmpty())){
                string top = tStack.peek();
                push(top);
                tStack.pop();
            }
            return finded;
        }
        void addStart(const string &val){
            int tSize = size();
            Stack tStack(tSize);
            while(!(isEmpty())){
                string top = peek();
                tStack.push(top);
                pop();
            }
            push(val);
            while(!(tStack.isEmpty())){
                string top = tStack.peek();
                push(top);
                tStack.pop();
            }
        }
        void addAfter(const string &mark, const string &val){
            if(search(mark)){
                int tSize = size();
                Stack tStack(tSize);
                while(!(isEmpty())){
                    string top = peek();
                    if(top == mark){
                        break;
                    }
                    tStack.push(top);
                    pop();
                }
                push(val);
                while(!(tStack.isEmpty())){
                    string top = tStack.peek();
                    push(top);
                    tStack.pop();
                }
            } else {
                cout << "Stack doesn't has element - " << mark << endl;
            }

        }
        void deleteSpecific(const string $val){
            if(search($val)){
                int tSize = size();
                Stack tStack(tSize);
                while(!(isEmpty())){
                    string top = peek();
                    if(top == $val){
                        break;
                    }
                    tStack.push(top);
                    pop();
                }
                pop();
                while(!(tStack.isEmpty())){
                    string top = tStack.peek();
                    push(top);
                    tStack.pop();
                }
            } else {
                cout << "Stack doesn't has element - " << $val << endl;
            }
        }
};

float get_time_passed(system_clock::time_point start){
    auto end = system_clock::now();
    microseconds time_passed = duration_cast<microseconds>(end - start);
    
    cout << "Time passed: " << time_passed.count() << endl;
    return time_passed.count();
}

void filling_up_the_stack(string arrayOfStrings[], Stack& stack, int endingSize){
    for(int i = stack.size(); i<endingSize; i++){
        stack.push(arrayOfStrings[i]);
    }
}

int main(){
    int size = 0;
    ifstream file("program_text.data");
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

	// Filling the initial array and deleting all spaces and invisible symbols
    string* arrayOfStrings = new string[size];
    ifstream file1("program_text.data");
    if (file1.is_open()) {
        int i = 0;
        while (getline(file1, line)) {
            if (i < size) {
                size_t start = line.find_first_not_of(" \t\n\r\f\v");
                if (start == string::npos){
                    arrayOfStrings[i] = "";
                } else {
                    size_t end = line.find_last_not_of(" \t\n\r\f\v");
                    if (!line.empty() && line.back() == '\r') line.pop_back();
                    arrayOfStrings[i] = line.substr(start, end - start + 1);
                }
                i++;
            } else {
                break;
            }
        }
        file1.close();
    } else {
        cerr << "Unable to open file!" << endl;
    }

    // sizes used in program
    int sizes[] = {size/3, size/3*2, size};

    // Stack instance for the whole programm
    Stack stack(size+10);
    
    // Variable for showing how much time each function is running, to calculate average
    int times_repeated = 100;
    // Array to store all average time
    int allAverageTime[18]; // 3 times 6 functions
    int average = 0;
    // Collecting data
    for(int i = 0; i < 3; i++){
        filling_up_the_stack(arrayOfStrings, stack, sizes[i]);
        average = 0;
        for (int j = 0; j<times_repeated; j++){
            auto start = system_clock::now();
            stack.display();
            average += get_time_passed(start);
        }
        cout << "Average time for displaying " << sizes[i] << " elements is: " << average/times_repeated << endl;
        allAverageTime[i*6+0] = average/times_repeated;
        average = 0;
        for (int j = 0; j<times_repeated; j++){
            auto start = system_clock::now();
            stack.search("def"); // everythere it is at the start that mean that size will afect the speed of the function
            average += get_time_passed(start);
        }
        allAverageTime[i*6+1] = average/times_repeated;
        cout << "Average time for searching for a specific element in the stack with " << sizes[i] << " elements in the stack is: " << average/times_repeated << endl;
        average = 0;
        for (int j = 0; j<times_repeated; j++){
            auto start = system_clock::now();
            stack.push("hi");
            average += get_time_passed(start);
            stack.pop();
        }
        allAverageTime[i*6+2] = average/times_repeated;
        cout << "Average time for adding at the end of " << sizes[i] << " size of stack is: " << average/times_repeated << endl;
        average = 0;
        for (int j = 0; j<times_repeated; j++){
            auto start = system_clock::now();
            stack.addStart("hi");
            average += get_time_passed(start);
            stack.deleteSpecific("hi");
        }
        allAverageTime[i*6+3] = average/times_repeated;
        cout << "Average time for adding element to the start of the stack with " << sizes[i] << " elements is: " << average/times_repeated << endl;
        average = 0;
        for (int j = 0; j<times_repeated; j++){
            auto start = system_clock::now();
            stack.addAfter("def", "hi");
            average += get_time_passed(start);
            stack.deleteSpecific("hi");
        }
        allAverageTime[i*6+4] = average/times_repeated;
        cout << "Average time for adding after a specific element in the stack with " << sizes[i] << " elements is: " << average/times_repeated << endl;
        average = 0;
        for (int j = 0; j<times_repeated; j++){
            stack.addStart("hi");
            auto start = system_clock::now();
            stack.deleteSpecific("hi");
            average += get_time_passed(start);
        }
        allAverageTime[i*6+5] = average/times_repeated;
        cout << "Average time for displaying " << sizes[i] << " elements is: " << average/times_repeated << endl;
    }
    // Printing results
    for(int i = 0; i < 3; i++){
        cout << "Average time for displaying stack with " << sizes[i] << " elements after " << times_repeated << " times repeadted is:" << endl;
        cout << allAverageTime[i*6+0] << endl;
    }
    for(int i = 0; i < 3; i++){
        cout << "Average time for searching element in the stack with " << sizes[i] << " elements after " << times_repeated << " times repeadted is:" << endl;
        cout << allAverageTime[i*6+1] << endl;
    }
    for(int i = 0; i < 3; i++){
        cout << "Average time for adding a new element at the end of the stack with " << sizes[i] << " elements after " << times_repeated << " times repeadted is:" << endl;
        cout << allAverageTime[i*6+2] << endl;
    }
    for(int i = 0; i < 3; i++){
        cout << "Average time for adding a new element at the start of the stack with " << sizes[i] << " elements after " << times_repeated << " times repeadted is:" << endl;
        cout << allAverageTime[i*6+3] << endl;
    }
    for(int i = 0; i < 3; i++){
        cout << "Average time for adding a new element at after a specific element of the stack with " << sizes[i] << " elements after " << times_repeated << " times repeadted is:" << endl;
        cout << allAverageTime[i*6+4] << endl;
    }
    for(int i = 0; i < 3; i++){
        cout << "Average time for deleting a specific element from the stack with " << sizes[i] << " elements after " << times_repeated << " times repeadted is:" << endl;
        cout << allAverageTime[i*6+5] << endl;
    }

    return 0;
}
