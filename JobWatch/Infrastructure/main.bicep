targetScope = 'subscription'

@description('Resource group name')
param resourceGroupName string = 'JobWatchRG'

@description('Azure region for all resources')
param location string = 'swedencentral'

@description('Storage account name (3-24 lowercase alphanumeric)')
param storageAccountName string = 'jobwatchstorage'

@description('File share name')
param fileShareName string = 'jobwatch'

@description('Container Apps environment name')
param containerAppEnvName string = 'jobwatch-env'

@description('Container App name')
param containerAppName string = 'jobwatch'

@description('Storage binding name in Container Apps env')
param envStorageName string = 'jobwatchfiles'

@description('Container image (e.g. dockerhubuser/jobwatch:v1)')
param containerImage string = 'kakan35783/jobwatch:v1'

@description('Mount path inside the container')
param mountPath string = '/mounts/jobwatch'

resource rg 'Microsoft.Resources/resourceGroups@2024-03-01' = {
  name: resourceGroupName
  location: location
}

module resources 'resources.bicep' = {
  scope: rg
  name: 'jobwatch-resources'
  params: {
    location: location
    storageAccountName: storageAccountName
    fileShareName: fileShareName
    containerAppEnvName: containerAppEnvName
    containerAppName: containerAppName
    envStorageName: envStorageName
    containerImage: containerImage
    mountPath: mountPath
  }
}

output appFqdn string = resources.outputs.appFqdn
output appUrl string = 'https://${resources.outputs.appFqdn}'
