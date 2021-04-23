# azure-webapp-Mgd-id

I was experimenting with Azure Managed Identities and working up a demo when I came across this article from https://tech-tutes.com/2021/03/18/access-azure-blob-storage-using-azure-ad-managed-identity/.  It demonstrates Azure Storage Container and blob creation using a Managed Identity in Azure.

This is just a working implementation of the code in that article squirreled away for future use.

Requires:

- Azure subscription
- Azure Resource Group
- Azure Application Service Plan
- Azure Webapp
- Azure Storage Account

I amended the code to allow you to create a message on a ServiceBus queue to demonstrate that working under Managed Identity also

Additional Requirements:

- Azure ServiceBus 
- Azure ServiceBus queue

In the code look for HomeController.cs and replace

"<Input StorageAccount>" with the name of your storage account in ""
"<Input ContainerName>" with the name of your container in ""
"<Input ServiceBus Name>" with just the name of your ServiceBus in "",  servicebus.windows.net is not required
"<Input queueName>" with the name of your queue in ""

The article explains the rest of the steps for Storage Accounts.  For other facilities I add it will be similar.

Note this app doesn't provide any Authentication so be sure and stop the App Service, remove the resources, or alternatively turn on Authentication for your Azure Tenant to prevent misuse.  The choice is yours.

