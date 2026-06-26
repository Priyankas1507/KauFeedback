FROM jenkins/jenkins:lts

USER root

RUN apt-get update && apt-get install -y \
    curl \
    wget \
    git \
    unzip \
    ca-certificates \
    gnupg \
    lsb-release

RUN curl -fsSL https://download.docker.com/linux/debian/gpg | \
    gpg --dearmor -o /usr/share/keyrings/docker.gpg

RUN echo \
"deb [arch=$(dpkg --print-architecture) signed-by=/usr/share/keyrings/docker.gpg] https://download.docker.com/linux/debian \
$(. /etc/os-release && echo $VERSION_CODENAME) stable" \
> /etc/apt/sources.list.d/docker.list

RUN apt-get update && apt-get install -y docker-ce-cli

USER jenkins