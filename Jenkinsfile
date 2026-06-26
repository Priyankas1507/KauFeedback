pipeline {
    agent any

    stages {

        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Build API') {
            steps {
                dir('KauFeedback.API') {
                    sh 'docker build -t kaufeedback-api .'
                }
            }
        }

        stage('Build Frontend') {
            steps {
                dir('kaufeedback-ui') {
                    sh 'docker build -t kaufeedback-frontend .'
                }
            }
        }

        stage('Deploy') {
            steps {
                sh 'docker compose up -d'
            }
        }

        stage('Verify') {
            steps {
                sh 'docker ps'
            }
        }
    }
}