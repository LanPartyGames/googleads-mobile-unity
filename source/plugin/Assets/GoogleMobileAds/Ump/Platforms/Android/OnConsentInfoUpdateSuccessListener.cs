#if UNITY_ANDROID
// Copyright 2022 Google LLC.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using UnityEngine;

namespace GoogleMobileAds.Ump.Android
{
    public class OnConsentInfoUpdateSuccessListener : AndroidJavaProxy
    {
        private Action onConsentInfoUpdateSuccessAction;

        public OnConsentInfoUpdateSuccessListener(Action onConsentInfoUpdateSuccessAction)
            : base(Utils.OnConsentInfoUpdateSuccessListenerClassName)
        {
            this.onConsentInfoUpdateSuccessAction = onConsentInfoUpdateSuccessAction;
        }

#region Callbacks from ConsentInformation

        // Proxy method for onConsentInfoUpdateSuccess in Android UMP SDK.
        internal void onConsentInfoUpdateSuccess()
        {
            if (onConsentInfoUpdateSuccessAction == null)
            {
                return;
            }
            // Run the success callback, if available.
            onConsentInfoUpdateSuccessAction();
        }

#endregion
    }
}
#endif
