FROM maven:3.9.4-eclipse-temurin-17 AS build
COPY . /app
WORKDIR /app
RUN mvn package -DskipTests

FROM ubuntu:latest
ENV JAVA_HOME=/opt/java/openjdk
COPY --from=eclipse-temurin:17 $JAVA_HOME $JAVA_HOME

ENV PATH="${JAVA_HOME}/bin:${PATH}"
RUN apt update 
RUN apt install -y ffmpeg

COPY --from=build /app/target/imsMedia.jar /app.jar
ENV SPRING_PROFILES_ACTIVE="docker"

EXPOSE 8080
ENTRYPOINT java -jar /app.jar
