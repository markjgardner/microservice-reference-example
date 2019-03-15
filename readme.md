# Microservice Reference Architecture

Using Azure Functions, API Management, Azure Storage Tables

## How to build/deploy this example?
Run ```deploy.ps1``` with the following parameters:
  * appName: The name of the microservice application (eg. employee)
  * resourceGroupName: The name of the resource group into which the components should be deployed. *Note: this resource group must already exist*
  * subscriptionId: The ID of the subscription to connect to when deploying these resources.