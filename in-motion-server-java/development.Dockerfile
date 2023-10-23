#FROM eclipse-temurin:17-jdk-jammy AS build
#ENV HOME=/usr/app
#RUN mkdir -p $HOME
#WORKDIR $HOME
#ADD . $HOME
#RUN --mount=type=cache,target=/root/.m2 ./mvnw -f $HOME/pom.xml clean package -DskipTests
#
#
#FROM ubuntu:latest
#ENV JAVA_HOME=/opt/java/openjdk
#COPY --from=eclipse-temurin:17 $JAVA_HOME $JAVA_HOME
#ENV PATH="${JAVA_HOME}/bin:${PATH}"
#RUN apt update 
#RUN apt install -y ffmpeg
#ARG JAR_FILE=/usr/app/target/*.jar
#COPY --from=build $JAR_FILE /app/runner.jar
#EXPOSE 8080
#ENTRYPOINT java -jar /app/runner.jar


FROM maven:3.9.4-eclipse-temurin-17 AS build
COPY . /app
WORKDIR /app
RUN mvn package -DskipTests

FROM eclipse-temurin:17-jdk-alpine
COPY --from=build /app/target/imsMedia.jar /app.jar
ENV SPRING_PROFILES_ACTIVE="docker"
RUN apk add ffmpeg

ENTRYPOINT ["java", "-jar", "/app.jar"]