//
//  osx_harness.h
//  CereprocMac
//
//  Created by Jenkins on 2/14/17.
//  Copyright © 2017 RAG. All rights reserved.
//

#ifndef osx_harness_h
#define osx_harness_h

#include "unity_callbacks.h"

    void CleanupTTS();
    void SpeakSSMLBlock(const char *text);
    void LoadTTS(const char *resourceDirectory, const char *cacheDirectory, const char *voiceName);
    void SetListenerObject(const char *listener);
    void SetCallbacks(unityCallback eventCallback, unityCallback audioGenCallback, unityCallback errorCallback);


#endif /* osx_harness_h */
