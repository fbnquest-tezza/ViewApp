//def ReleaseDir = "c:\inetpub\wwwroot"
def projectName = "ViewApp"
pipeline {
			agent any
			stages {
				stage('Source'){
					steps{
						checkout([
						$class: 'GitSCM',
						branches: [[name: '*/master']],
						doGenerateSubmoduleConfigurations: false,
						extensions: [],
						submoduleCfg: [],
						userRemoteConfigs: [[credentialsId: '', url: 'https://github.com/fbnquest-tezza/ViewApp.git']]
						])
					}
				}
				//stage('Nuget Package Restore'){
					//steps{
					
					
					//bat label: 'Restore Nuget', script: 'nuget restore '
					//}
				//}
				stage('Build Deploy') {
    					steps {
						
							sh """
							dotnet build -c Release /p:Version=${BUILD_NUMBER} /p:Platform=“Any CPU”
							//run the first dotnet build. If successsful, append and run the second build
							dotnet build 
							/p:DeployOnBuild=true  													
							/p:SkipInvalidConfigurations=true 
							/t:build 
							/p:precompilebeforepublish=true							
							/p:DeleteExistingFiles=True 
							//if successful, run the dotnet publish							
							dotnet publish 
							/p:DeployDefaultTarget=WebPublish 
							/p:WebPublishMethod=MSDeploy   
							/p:MSDeployPublishMethod=InProc   
							/p:MSDeployServiceURL=localhost 
							/p:DeployIisAppPath=“Default Web Site/CMS”
									"""
		}
		}
				

			}
}

