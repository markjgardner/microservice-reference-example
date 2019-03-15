param(
  [Parameter(Mandatory=$true)]
  $resourceGroupName,
  
  [Parameter(Mandatory=$true)]
  $appName,

  [Parameter(Mandatory=$true)]
  $subscriptionId 
)

write-output("Connecting to the target azure subscription...")
Connect-AzAccount
Select-AzSubscription -Subscription $subscriptionId

$params = @{
    appName = "$appName"
    location = "NorthCentralUS"
    apiPublisherEmail = "null@email.com"
    apiPublisherName = "Contoso Inc."
}

$rg = Get-AzResourceGroup -Name $resourceGroupName

write-output "Deploying resources..."
New-AzResourceGroupDeployment -Name "Employee Microservice Deployment" -ResourceGroupName $resourceGroupName -TemplateParameterObject $params -TemplateFile "armtemplate.json" -Mode Incremental
write-output "completed"