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
							dotnet build -c Release /p:Version=${BUILD_NUMBER}
							/p:DeployOnBuild=true 
							/p:DeployDefaultTarget=WebPublish 
							/p:WebPublishMethod=MSDeploy
							/p:MSDeployPublishMethod=InProc 
							/p:MSDeployServiceURL=localhost 
							/p:DeployIisAppPath=“Default Web Site/CMS”
							/p:SkipInvalidConfigurations=true 
							/t:build 
							/p:precompilebeforepublish=true
							/t:publish 
							/p:Configuration=Release 
							/p:Platform=“Any CPU”
							/p:DeleteExistingFiles=True 
							/p:publishUrl=c:\\inetpub\\wwwroot\\CMS
									"""
		}
				}
				

			}
}

