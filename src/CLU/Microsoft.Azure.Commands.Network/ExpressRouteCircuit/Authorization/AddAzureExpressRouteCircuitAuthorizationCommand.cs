﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Commands.Network.Models;


namespace Microsoft.Azure.Commands.Network
{
    [Cmdlet(VerbsCommon.Add, "AzureRmExpressRouteCircuitAuthorization"), OutputType(typeof(PSExpressRouteCircuit))]
    [CliCommandAlias("expressroute;circuit;authorization;add")]
    public class AddAzureExpressRouteCircuitAuthorizationCommand : AzureExpressRouteCircuitAuthorizationBase
    {
        [Parameter(
           Mandatory = true,
           ValueFromPipeline = true,
           HelpMessage = "The Circuit")]
        public PSExpressRouteCircuit Circuit { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            var authorization = this.Circuit.Authorizations.SingleOrDefault(resource => string.Equals(resource.Name,this.Name, System.StringComparison.CurrentCultureIgnoreCase));

            if (authorization != null)
            {
                throw new ArgumentException("Authorization with the specified name already exists");
            }

            authorization = new PSExpressRouteCircuitAuthorization();

            authorization.Name = this.Name;
            this.Circuit.Authorizations.Add(authorization);

            WriteObject(this.Circuit);
        }
    }
}