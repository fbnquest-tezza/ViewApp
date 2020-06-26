node {
 def svn_credentialId = "2d7f5f55-6dd2-5t6y-8b98-3a22817b70e4"
 //def svn_project_root = "https://example.com/svn/Repo/ToDoApp/trunk"
 def svn_project_root = "https://github.com/fbnquest-tezza/ViewApp.git"
 //def slnPath = "${WORKSPACE}\\trunk"
 def slnPath = "${WORKSPACE}\\ViewApp"
 //def slnName = "ToDoApp.sln"
 def slnName = "ViewApplication.sln"
 //def projPath = "ToDoApp"
 def projPath = "ViewApp"
 //def projName = "ToDoApp.Web.csproj"
 def projName = "ViewApplication.csproj"
 //def MsBuildPath = "C:\\Program Files (x86)\\MSBuild\\12.0\\Bin\\MSBuild.exe"
 def MsBuildPath = "C:\\Program Files (x86)\\MSBuild\\14.0\\Bin\\MSBuild.exe"
 //def MsDeployPath = "C:\\Program Files\\IIS\\Microsoft Web Deploy V3\\msdeploy.exe"
 def MsDeployPath = "C:\\Program Files\\IIS\\Microsoft Web Deploy V3\\msdeploy.exe"
 //def packagePath = "${projPath}\\obj\\Release\\_PublishedWebsites\\ToDoApp.Web_Package\\ToDoApp.Web.zip"
 def packagePath = "${projPath}\\obj\\Release\\_PublishedWebsites\\ViewApp.Web_Package\\ViewApp.Web.zip"
// def IISWebPath = "ToDoApp"
 def IISWebPath = "ViewApp"
 def server_dev = "DEV_Server"
 def server_qa = "QA_Server"
 def server_admin_userName = "domain\\userName"
 def server_admin_pwd = "SecretPassword"
 def nuget_path = "C:\\projects\\Nuget\\nuget.exe"
 def set_param_QA = "C:\\Deployment\\ToDoApp\\QA.ToDoApp.Web.SetParameters.xml"

 try {
  stage('Checkout') {
   checkout([
   						$class: 'GitSCM',
   						branches: [[name: '*/master']],
   						doGenerateSubmoduleConfigurations: false,
   						extensions: [],
   						submoduleCfg: [],
   						userRemoteConfigs: [[credentialsId: '', url: 'https://github.com/fbnquest-tezza/ViewApp.git']]
   						])
  }

  dir(slnPath) {
   stage('Nuget Restore') {
    bat " \"${nuget_path}\" config -set http_proxy=http://proxy.example.com:8080"
    bat " \"${nuget_path}\" config -set http_proxy.user=${server_admin_userName}"
    bat " \"${nuget_path}\" config -set http_proxy.password=${server_admin_pwd}"
    bat " \"${nuget_path}\" restore \"${slnPath}\\${slnName}\" "
   }

   stage('Build & Package') {
    bat " \"${MsBuildPath}\" \"${projPath}\\${projName}\" /T:Build;Package /p:Configuration=RELEASE /p:OutputPath=\"obj\\Release\" /p:DeployIIsAppPath=\"${IISWebPath}\" /p:VisualStudioVersion=12.0"
   }

   stage('Archive Artifacts') {
    archiveArtifacts "${packagePath}"
   }

   stage('Deployment to Dev') {
    bat " \"${MsDeployPath}\" -verb:sync -source:contentPath=\"${slnPath}\\${projPath}\\App_offline-template.htm\" -dest:contentPath=\"${IISWebPath}/App_offline.htm\",computerName=${server_dev},userName=${server_admin_userName},passWord=${server_admin_pwd} -allowUntrusted=true"
    bat " \"${MsDeployPath}\" -verb:sync -source:package=\"${packagePath}\" -dest:auto,computerName=${server_dev},userName=${server_admin_userName},passWord=${server_admin_pwd} -allowUntrusted=true -enablerule:AppOffline"
   }

   stage('Approval - QA Deploy') {
    input "Deploy to QA?"
   }

   stage('Deployment to QA') {
    bat " \"${MsDeployPath}\" -verb:sync -source:contentPath=\"${slnPath}\\${projPath}\\App_offline-template.htm\" -dest:contentPath=\"${IISWebPath}/App_offline.htm\",computerName=${server_qa},userName=${server_admin_userName},passWord=${server_admin_pwd} -allowUntrusted=true"
    bat " \"${MsDeployPath}\" -verb:sync -source:package=\"${packagePath}\" -dest:auto,computerName=${server_qa},userName=${server_admin_userName},passWord=${server_admin_pwd} -allowUntrusted=true -enablerule:AppOffline -setParamFile:${set_param_QA}"
   }
  }
  notify('Success')
 } catch (err) {
  notify("Error ${err}")
  currentBuild.result = 'FAILURE'
 }
}

def notify(status) {
 emailext(to: "deleogold@gmail.com",
  subject: "${status}: Deployment job '${env.JOB_NAME} [${env.BUILD_NUMBER}]'",
  body: "<p>${status}: Deployment Job '${env.JOB_NAME} [${env.BUILD_NUMBER}]':</p> <p>Check console output at <a href='${env.BUILD_URL}'>${env.JOB_NAME} [${env.BUILD_NUMBER}]</a></p>"
  )
}


////def ReleaseDir = "c:\inetpub\wwwroot"
//def projectName = "ViewApp"
//pipeline {
//			agent any
//			stages {
//				stage('Source'){
//					steps{
//						checkout([
//						$class: 'GitSCM',
//						branches: [[name: '*/master']],
//						doGenerateSubmoduleConfigurations: false,
//						extensions: [],
//						submoduleCfg: [],
//						userRemoteConfigs: [[credentialsId: '', url: 'https://github.com/fbnquest-tezza/ViewApp.git']]
//						])
//					}
//				}
//				//stage('Nuget Package Restore'){
//					//steps{
//
//
//					//bat label: 'Restore Nuget', script: 'nuget restore '
//					//}
//				//}
//				stage('Donet restore') {
//    					steps {
//
//							bat """
//								dotnet build -c Release /p:Version=${BUILD_NUMBER}
//								dotnet publish -c Release --no-build
//							"""
//		}
//			}
//
//			stage('MSBuild') {
//    				steps {
//
//
//						    bat """
//
//							${tool 'MSBuild'} ViewApplication.sln
//							/p:DeployOnBuild=true
//							/p:DeployDefaultTarget=WebPublish
//							/p:WebPublishMethod=MSDeploy
//							/p:SkipInvalidConfigurations=true
//							/t:build
//							/p:Configuration=Release
//							/p:Platform=\"Any CPU\"
//							/p:DeleteExistingFiles=True
//							/p:MSDeployPublishMethod=InProc
//							/p:MSDeployServiceURL=localhost
//							/p:DeployIisAppPath=\"Default Web Site/CMS
//							/p:publishUrl=c:\\inetpub\\wwwroot\\CMS
//
//							"""
//
//    					}
//			}
//
//    //stage('Deploy'){
//      //  steps{
//
//		//	withCredentials([
//			//	usernamePassword(
//				//	credentialsId: 'iis-credential',
//			//		usernameVariable: 'USERNAME',
//			//		passwordVariable: 'PASSWORD'
//			//		)])
//			//		{
//			//		bat """ "C:\\Program Files (x86)\\IIS\\Microsoft Web Deploy V3\\msdeploy.exe" -verb:sync -source:iisApp="${WORKSPACE}\\${publishedPath}" -enableRule:AppOffline -dest:iisApp="${iisApplicationName}",ComputerName="https://${targetServerIP}:8172/msdeploy.axd",UserName="$USERNAME",Password="$PASSWORD",AuthType="Basic" -allowUntrusted"""}
//		//}
//   // }
//			}
//}


