//
//  main.c
//  CereprocMacTest
//
//  Created by Jenkins on 2/14/17.
//  Copyright © 2017 RAG. All rights reserved.
//

#include <stdio.h>
#include "osx_harness.h"

void print(const char *a, const char *b)
{
    printf(a);
    printf("\n");
    printf(b);
    printf("\n");
}

int main(int argc, const char * argv[]) {
    
    SetListenerObject("TestHarness");
    SetCallbacks(print, print, print);
    
    LoadTTS("/Users/jenkins/unityclient/Assets/CerevoiceFiles/", "/Users/jenkins/", "heather");
    SpeakSSMLBlock("<speech>Hello, which project do you wish to test?</speech>");
    CleanupTTS();
    
    return 0;
}
