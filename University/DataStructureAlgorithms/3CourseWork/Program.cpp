#include <iostream>
#include <map>
#include <string>
#include <vector>
#include <algorithm>

using namespace std;


string traverseGraph(map<string, vector<string>>& incidenceList, vector<string>& visitedVerteces, string traverseResult, string index, int& compCount){
    traverseResult += " " + index;
    visitedVerteces.push_back(index);
    for(string vertex : incidenceList[index]){
        compCount ++;
        auto it = find(visitedVerteces.begin(), visitedVerteces.end(), vertex);
        compCount ++;
        // If vertex is not in the visetedVerteces
        if(it == visitedVerteces.end()) {
            traverseResult = traverseGraph(incidenceList, visitedVerteces, traverseResult, vertex, compCount);
        }
    }
    return traverseResult;
}

string preporationForTraverse(map<string, vector<string>>& incidenceList){
    vector<string> visitedVerteces;
    string traverseResult;
    string index = incidenceList.begin()->first;
    int compCount = 0;
    string result = traverseGraph(incidenceList, visitedVerteces, traverseResult, index, compCount);    
    cout << "Count of comparisons " << compCount << " for " << visitedVerteces.size() << " size of graph." << endl;
    return result;
} 

int main(){
    // Initial matrix of the grapth
    int incidenceMatrix[14][12] = {
//       0  1  2  3  4  5  6  7  8  9  10 11
    {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0}, // 0 - 4
    {0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0}, // 1 - 2
    {0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0}, // 1 - 4
    {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0}, // 1 - 6
    {0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0}, // 2 - 3
    {0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0}, // 2 - 5
    {0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0}, // 2 - 7
    {0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0}, // 3 - 7
    {0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0}, // 4 - 9
    {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0}, // 6 - 9
    {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1}, // 6 - 11
    {0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1}, // 7 – 11
    {0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0}, // 8 – 9
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1}}; // 10 - 11
    // Initial incidence list
    map<string, vector<string>> incidenceList1 = { {"0", {"4"}},
                                                   {"1", {"2", "4", "6"}},
                                                   {"2", {"1", "3", "5", "7"}},
                                                   {"3", {"2", "7"}},
                                                   {"4", {"0", "1", "9"}},
                                                   {"5", {"2"}},
                                                   {"6", {"1", "9", "11"}},
                                                   {"7", {"2", "3", "11"}},
                                                   {"8", {"9"}},
                                                   {"9", {"4", "6", "8"}},
                                                   {"10", {"11"}},
                                                   {"11", {"6", "7", "10"}}};
    // Final variant of list to get my name as a traverse result
    map<string, vector<string>> incidenceListName = { {"A", {"N"}},
                                                   {"T", {"K", "N", "C"}},
                                                   {"K", {"T", "U", "n", "R"}},
                                                   {"U", {"K", "R"}},
                                                   {"N", {"A", "T", "H"}},
                                                   {"n", {"K"}},
                                                   {"C", {"T", "H", "O"}},
                                                   {"R", {"K", "U", "O"}},
                                                   {"k", {"H"}},
                                                   {"H", {"N", "C", "k"}},
                                                   {"I", {"O"}},
                                                   {"O", {"C", "R", "I"}}};
    // Extended inicial incidence list
    map<string, vector<string>> incidenceList2 = { {"0", {"4", "12", "13"}},
                                                   {"1", {"2", "4", "6", "13"}},
                                                   {"2", {"1", "3", "5", "7", "13"}},
                                                   {"3", {"2", "7", "13", "14"}},
                                                   {"4", {"0", "1", "9", "12"}},
                                                   {"5", {"2"}},
                                                   {"6", {"1", "9", "11"}},
                                                   {"7", {"2", "3", "11", "14"}},
                                                   {"8", {"9", "12", "15"}},
                                                   {"9", {"4", "6", "8", "15"}},
                                                   {"10", {"11", "15"}},
                                                   {"11", {"6", "7", "10", "14", "15"}},
                                                   {"12", {"0", "4", "8"}},
                                                   {"13", {"0", "1", "2", "3"}},
                                                   {"14", {"3", "7", "11"}},
                                                   {"15", {"8", "9", "10", "11"}}};
    // More extended incidece list
    map<string, vector<string>> incidenceList3 = { {"0", {"4", "12", "13"}},
                                                   {"1", {"2", "4", "6", "13"}},
                                                   {"2", {"1", "3", "5", "7", "13"}},
                                                   {"3", {"2", "7", "13", "14"}},
                                                   {"4", {"0", "1", "9", "12"}},
                                                   {"5", {"2"}},
                                                   {"6", {"1", "9", "11"}},
                                                   {"7", {"2", "3", "11", "14"}},
                                                   {"8", {"9", "12", "15"}},
                                                   {"9", {"4", "6", "8", "15"}},
                                                   {"10", {"11", "15"}},
                                                   {"11", {"6", "7", "10", "14", "15"}},
                                                   {"12", {"0", "4", "8", "16"}},
                                                   {"13", {"0", "1", "2", "3", "17"}},
                                                   {"14", {"3", "7", "11", "18"}},
                                                   {"15", {"8", "9", "10", "11", "19"}},
                                                   {"16", {"12"}},
                                                   {"17", {"13"}},
                                                   {"18", {"14"}},
                                                   {"19", {"15"}}};

    // Printing incidence Matrix
    cout << endl;
    cout << "Incidence Matrix:" << endl;
    for(int i = 0; i < 14; i++){
        cout << "| ";
        for(int j = 0; j < 12; j++){
            cout << incidenceMatrix[i][j] << " ";
        }
        cout << "|" << endl;
    }
     
    // Printing inicial incidence list
    cout << endl;
    cout << "Incidence list:" << endl;
    for (map<string, vector<string>>::iterator it = incidenceList1.begin(); it != incidenceList1.end() ; it++){
        cout << it->first << ": ";
        for(int j = 0; j < it->second.size(); j++){
            cout << it->second[j] << " ";
        }
        cout << endl;
    }
    cout << endl;

    cout << preporationForTraverse(incidenceList1) << endl;
    cout << preporationForTraverse(incidenceListName) << endl;
    cout << preporationForTraverse(incidenceList2) << endl;
    cout << preporationForTraverse(incidenceList3) << endl;
    return 0;
}
