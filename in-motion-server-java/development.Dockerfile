FROM ubuntu:latest
ENV JAVA_HOME=/opt/java/openjdk
COPY --from=eclipse-temurin:17 $JAVA_HOME $JAVA_HOME
ENV PATH="${JAVA_HOME}/bin:${PATH}"
COPY ./ ./app
RUN apt update 
RUN apt install -y ffmpeg
WORKDIR ./app
RUN ./mvnw package -DskipTests

ENTRYPOINT ["java", "-jar", "/app/target/in-motion-server-java-1.0.0.jar"]
