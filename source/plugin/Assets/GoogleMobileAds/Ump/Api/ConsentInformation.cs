// Copyright (C) 2022 Google LLC.
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

using GoogleMobileAds.Ump.Common;

using System;
using UnityEngine;

namespace GoogleMobileAds.Ump.Api
{
    /// <summary>
    /// Utility methods for managing consent information from users.
    /// </summary>
    public class ConsentInformation
    {
        internal static IUmpClientFactory clientFactory;
        private static IConsentInformationClient _client;

        /// <summary>
        /// The user's consent status.
        /// This value is cached between app sessions and can be read before
        /// requesting updated parameters.
        /// </summary>
        public static ConsentStatus ConsentStatus
        {
            get
            {
                _client = GetClientFactory().ConsentInformationClient();
                return (ConsentStatus)_client.GetConsentStatus();
            }
        }

        /// <summary>
        /// Requests consent information update.
        /// </summary>
        /// <param name="request">The request params.</param>
        /// <param name="onSuccess">Called when ConsentStatus is updated.
        /// <param name="onError">Called on consent form request error.
        /// Includes a FormError argument if show failed. </param>
        public static void RequestConsentInfoUpdate(ConsentRequestParameters request,
                                                    Action onSuccess,
                                                    Action<FormError> onError)
        {
            _client = GetClientFactory().ConsentInformationClient();
            _client.RequestConsentInfoUpdate(request, onSuccess, onError);
        }

        /// <summary>
        /// Clears all consent status from persistent storage.
        /// </summary>
        public static void ResetInfo()
        {
            _client = GetClientFactory().ConsentInformationClient();
            _client.ResetInfo();
        }

        /// <summary>
        /// Gets the current consent status.
        /// <para>This value is cached between app sessions and can be read before
        /// requesting updated parameters.</para>
        /// </summary>
        public static ConsentStatus GetConsentStatus()
        {
            _client = GetClientFactory().ConsentInformationClient();
            return (ConsentStatus)_client.GetConsentStatus();
        }

        /// <summary>
        /// Returns <c>true</c> if a
        /// <see cref="GoogleMobileAds.Ump.Api.ConsentForm">ConsentForm</see>
        /// is available, otherwise returns <c>false</c>.
        /// </summary>
        public static bool IsConsentFormAvailable()
        {
            _client = GetClientFactory().ConsentInformationClient();
            return _client.IsConsentFormAvailable();
        }

        /// <summary>
        /// Creates and sets a client factory instance of the identified platform, if exists.
        /// </summary>
        internal static IUmpClientFactory GetClientFactory()
        {
            if (clientFactory == null)
            {
                string typeName;
                if (Application.platform == RuntimePlatform.IPhonePlayer)
                {
                    typeName = "GoogleMobileAds.Ump.iOS.UmpClientFactory,GoogleMobileAds.iOS";
                }
                else if (Application.platform == RuntimePlatform.Android)
                {
                    typeName =
                            "GoogleMobileAds.Ump.Android.UmpClientFactory,GoogleMobileAds.Android";
                }
                else if (Application.platform == RuntimePlatform.OSXEditor ||
                         Application.platform == RuntimePlatform.WindowsEditor)
                {
                    typeName = "GoogleMobileAds.Ump.Unity.UmpClientFactory,GoogleMobileAds.Unity";
                }
                else
                {
                    typeName = null;
                    Debug.Log("Platform not supported.");
                }
                Type type = Type.GetType(typeName);
                clientFactory = (IUmpClientFactory)System.Activator.CreateInstance(type);
            }
            return clientFactory;
        }
    }
}
