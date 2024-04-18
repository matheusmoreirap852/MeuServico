#!/bin/bash

AWS_PROFILE="aws-matheus"
AWS_REGION="us-east-1"
ECR_REPO="590125742949.dkr.ecr.us-east-1.amazonaws.com"
HOST_DEV=54.145.17.164
PATH_KEY="/mnt/keydev/keydev.pem"

aws ecr get-login-password --region "$AWS_REGION" --profile "$AWS_PROFILE" | docker login --username AWS --password-stdin "$ECR_REPO"

if [ "$1" == "auth" ]; then
    docker build -t auth -f Auth/Dockerfile_Auth Auth
    docker tag auth:latest "$ECR_REPO/auth:latest"
    docker push "$ECR_REPO/auth:latest"

elif [ "$1" == "app" ]; then
    docker build -t app -f ProjetoServicoWork/Dockerfile_App ProjetoServicoWork
    docker tag app:latest "$ECR_REPO/meuservico:latest"
    docker push "$ECR_REPO/meuservico:latest"

elif [ "$1" == "api" ]; then
    docker build -t api -f BackEndApi/Dockerfile_Api BackEndApi
    docker tag api:latest "$ECR_REPO/meuservicoapi:latest"
    docker push "$ECR_REPO/meuservicoapi:latest"

elif [ "$1" == "all" ]; then
    docker build -t auth -f Auth/Dockerfile_Auth Auth
    docker tag auth:latest "$ECR_REPO/auth:latest"
    docker push "$ECR_REPO/auth:latest"

    docker build -t app -f ProjetoServicoWork/Dockerfile_App ProjetoServicoWork
    docker tag app:latest "$ECR_REPO/meuservico:latest"
    docker push "$ECR_REPO/meuservico:latest"

    docker build -t api -f BackEndApi/Dockerfile_Api BackEndApi
    docker tag api:latest "$ECR_REPO/meuservicoapi:latest"
    docker push "$ECR_REPO/meuservicoapi:latest"

else
    echo "Uso: ./script.sh [auth|app|api|all]"
    exit 1
fi
ssh -i $PATH_KEY -o StrictHostKeyChecking=no ubuntu@$HOST_DEV 'bash deploy.sh'
