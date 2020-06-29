//def ReleaseDir = "c:\inetpub\wwwroot"
pipeline {
    agent {
        label 'master'
    }

    environment {
        PROJECT_NAME = 'ViewApplication'
        DOMAIN = 'mydomain.com'
        STACK = 'asp.net'
        DOCKER_REGISTRY = 'https://registry-1.docker.io/v2/'
//        DOCKER_REGISTRY = 'https://index.docker.io/v1/'
        CONTAINER = 'henryodinaka/viewapp'
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
                        def img = docker.build("${CONTAINER}:${VERSION}")
                        img.push()
                        sh "docker rmi ${img.id}"
                    }
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
