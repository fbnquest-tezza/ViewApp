//def ReleaseDir = "c:\inetpub\wwwroot"
def img
pipeline {
    agent {
        label 'master'
    }

    environment {
//        PATH = "C:\\Program Files\\Git\\usr\\bin;C:\\Program Files\\Git\\bin;${env.PATH}"
        PROJECT_NAME = 'ViewApplication'
        DOMAIN = 'mydomain.com'
        STACK = 'asp.net'
        // DOCKER_REGISTRY = 'https://registry-1.docker.io/v2/'
       DOCKER_REGISTRY = 'http://192.168.50.9:5000/v2/'
        CONTAINER = 'localhost:5000/viewapp'
        VERSION = "1.${BUILD_NUMBER}"
    }
//    try {
        stages {
            stage('Checkout from GitSCM') {
                steps {
                    checkout([
                            $class                           : 'GitSCM',
                            branches                         : [[name: '*/master']],
                            doGenerateSubmoduleConfigurations: false,
                            extensions                       : [],
                            submoduleCfg                     : [],
                            userRemoteConfigs                : [[credentialsId: '', url: 'https://github.com/fbnquest-tezza/ViewApp.git']]
                    ])
                }
            }

            stage('Build Image') {
                steps {
                    script {
                        docker.withRegistry("${DOCKER_REGISTRY}", 'docker-registry') {
                        echo"About to build"
                        img = docker.build("${CONTAINER}:${VERSION}")
                        echo "After build and before pushing"
                        img.push()
                        echo "after pushin"
                        sh "docker rmi ${img.id}"
                    }
                }
            }
        }
        stage( 'Pusblish UT Reports'){
            steps{
                script {
                    containerID = sh (
                    script: "docker run -d accountownerapp:B${BUILD_NUMBER}", 
                returnStdout: true
                ).trim()
                echo "Container ID is ==> ${containerID}"
                sh "docker cp ${containerID}:/TestResults/test_results.xml test_results.xml"
                sh "docker stop ${containerID}"
                sh "docker rm ${containerID}"
                step([$class: 'MSTestPublisher', failOnError: false, testResultsFile: 'test_results.xml'])    
                }
      
            }
        }
            stage('Approval - QA Deploy') {
                steps {
                    input "Deploy to QA?"
                }

            }
            stage('Deploy Stack') {
                steps {
                    withCredentials([
                            usernamePassword(
                                    credentialsId: 'docker-registry',
                                    usernameVariable: 'DOCKER_USER',
                                    passwordVariable: 'DOCKER_PASSWORD'
                            )
                    ])
                            {
                                script {
                                    echo "Deploying Container Stack to Docker Cluster"
                                    bat "ansible-playbook -i devops/inventories/manager1/hosts devops/manager1.yml --extra-vars=\"{'WORKSPACE': '${env.WORKSPACE}', 'DOMAIN': '${env.DOMAIN}', 'PROJECT': '${env.PROJECT}', 'STACK': '${env.STACK}, 'VERSION': '${env.VERSION}', 'DOCKER_REGISTRY': '${env.DOCKER_REGISTRY}, 'DOCKER_USER': '${env.DOCKER_USER}', 'DOCKER_PASSWORD': '${env.DOCKER_PASSWORD}'}\" -vvv"
                                }
                            }
                }
            }

        }
//    } catch (err) {
////        notify("Error occured in one of the build stages ${err}")er
//        currentBuild.result = 'FAILURE'
//    }
}

def notify(status) {
    emailext(to: "deleogold@gmail.com",
            subject: "${status}: Deployment job '${env.JOB_NAME} [${env.BUILD_NUMBER}]'",
            body: "<p>${status}: Deployment Job '${env.JOB_NAME} [${env.BUILD_NUMBER}]':</p> <p>Check console output at <a href='${env.BUILD_URL}'>${env.JOB_NAME} [${env.BUILD_NUMBER}]</a></p>"
    )
}
