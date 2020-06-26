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
						
							bat """
								dotnet restore 
								dotnet build -c Release /p:Version=${BUILD_NUMBER}
								dotnet build /p:DeployOnBuild=true /p:DeployDefaultTarget=WebPublish
								/p:WebPublishMethod=MSDeploy  
								/p:SkipInvalidConfigurations=true 
								/t:build 
								/p:Configuration=Release 
								/p:Platform=\"Any CPU\"
								/p:DeleteExistingFiles=True
								/p:publishUrl=c:\\inetpub\\wwwroot\\CMS"
								dotnet publish -c Release --no-build
								
								
								
								dotnet publish 
								/p:DeployDefaultTarget=WebPublish  
								/p:MSDeployPublishMethod=InProc   
								/p:MSDeployServiceURL=localhost 
								/p:DeployIisAppPath=“Default Web Site/CMS”
							"""
		}
				}
				
				//stage('Build last') {
    				//	steps {
						
							//bat "\"${tool 'MSBuild'}\" ViewApplication.sln /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
							
							//				    bat "\"${tool 'MSBuild'}\" ViewApplication.sln /p:DeployOnBuild=true /p:DeployDefaultTarget=WebPublish /p:WebPublishMethod=FileSystem /p:SkipInvalidConfigurations=true /t:build /p:Configuration=Release /p:Platform=\"Any CPU\" /p:DeleteExistingFiles=True /p:publishUrl=c:\\inetpub\\wwwroot"
    				//	}
			//	}
				
    //stage('Deploy'){
      //  steps{
		
		//	withCredentials([
			//	usernamePassword(
				//	credentialsId: 'iis-credential',
			//		usernameVariable: 'USERNAME',
			//		passwordVariable: 'PASSWORD'
			//		)]) 
			//		{ 
			//		bat """ "C:\\Program Files (x86)\\IIS\\Microsoft Web Deploy V3\\msdeploy.exe" -verb:sync -source:iisApp="${WORKSPACE}\\${publishedPath}" -enableRule:AppOffline -dest:iisApp="${iisApplicationName}",ComputerName="https://${targetServerIP}:8172/msdeploy.axd",UserName="$USERNAME",Password="$PASSWORD",AuthType="Basic" -allowUntrusted"""}
		//}
   // }
			}
}

