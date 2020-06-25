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
				stage('Build') {
    					steps {
						
						 bat """
        //cd ${projectName}
        dotnet build -c Release /p:Version=${BUILD_NUMBER}
        dotnet publish -c Release --no-build
        """
    					    //bat "\"${tool 'MSBuild'}\" ViewApplication.sln /p:DeployOnBuild=true /p:DeployDefaultTarget=WebPublish /p:WebPublishMethod=FileSystem ///p:SkipInvalidConfigurations=true /t:build /p:Configuration=Release /p:Platform=\"Any CPU\" /p:DeleteExistingFiles=True /p:publishUrl=c:\\inetpub\\wwwroot"
    					}
				}
			}
}

