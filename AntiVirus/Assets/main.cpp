#include <string>
#include <iostream>
#include <variant> 
#include <vector>
using namespace std;
using value = variant<int, string>;
using token = variant<string, value, char>; 

int main() {
    // Read the expression from STDIN
    // string expr;
    // cin >> expr;

    // TODO Parse expr and evaluate it
    // TODO write the result of the evaluation to STDOUT

}

/*
Approach
1. Check if we are examining the innermost function
    1. if yes -> move to step 2 and return the evaluation
    2. if no -> send the inner function to the inner function check
2. Determine which operation we are embarking on 
    6. vector(a0, .., aN) -> int[]
    7. range (start, stop) -> int[]
    8. addv(x, y) -> int[]
    9. reduce_add(v): (int[], int[]) -> int


take the first bit that decides what kind of function we are evaluating, and then recursively force the function arguments to be correct


*/

string parse(string function){
    if (get_op(function) == "add"){
        return add(function.substr(4, function.length() - 2));
    }
    if (get_op(function) == "sub"){
        
    }
    if (get_op(function) == "len"){
        
    }
    if (get_op(function) == "concat"){
        
    }
    if (get_op(function) == "parse"){
        
    }
}

int add(string function){
    if (innermost(function)){
        vector<string> vars = split_arg(function.substr(4, function.length() - 2));
        return stoi(vars[0]) + stoi(vars[1]);
    }
    else {

    }
}

int sub(string function){
    if (innermost(function)){
        vector<string> vars = split_arg(function.substr(4, function.length() - 2));
        return stoi(vars[0]) - stoi(vars[1]);
    }
}

int len(string str){
    return str.length();
}

string concat(string x, string y){
    return x + y;
}

int parse(string num){
    return stoi(num);
}

string get_op(string function){
    for (int i = 0; i < function.length(); i++){
        if (function[i] == '('){
            return function.substr(0, i);
        }
    }
}

// Note that this will probably break if you are dealing with strings that contain the characters "(" or ")"
bool innermost(string function){
    if (count(function.begin(), function.end(), '(') > 1){
        return false;
    }
    else {
        return true;
    }
}

std::vector<string> split_arg(string variables){
    std::vector<string> args;
    for(int i = 0; i < variables.length(); i++){
        if (i ==','){
            args.push_back(variables.substr(0, i-1));
            args.push_back(variables.substr(i+1, variables.length() - 1));
            return args;
        }
    }
}