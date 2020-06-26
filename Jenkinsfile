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
				stage('Build restore') {
    					steps {
						bat "dotnet build -c Release /p:Version=${BUILD_NUMBER} /p:Platform=“Any CPU”"
		}
				}
				

			}
}

