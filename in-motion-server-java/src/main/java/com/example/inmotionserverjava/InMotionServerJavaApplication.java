package com.example.inmotionserverjava;

import com.squareup.gifencoder.FloydSteinbergDitherer;
import com.squareup.gifencoder.GifEncoder;
import com.squareup.gifencoder.Image;
import com.squareup.gifencoder.ImageOptions;
import org.bytedeco.javacv.Frame;
import org.bytedeco.javacv.Java2DFrameConverter;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

import org.bytedeco.javacv.FFmpegFrameGrabber;
import org.springframework.stereotype.Component;

import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.concurrent.TimeUnit;

@SpringBootApplication
public class InMotionServerJavaApplication {

    public static void main(String[] args) {
        SpringApplication.run(InMotionServerJavaApplication.class, args);
    }

    // Image to array conversion time is about 5-8 milliseconds
    // Grab 0 -1 milliseconds
    // Convert to BufferedImage 5 - 8 milliseconds
    // Time problem in addImage do GifEncoder ~ 2000 milliseconds

    /** CONVERSION PROCESS
     * 1. Get file
     * 2. Get file data
     * 3. Set gif properties
     * 5. Set up output file
     * 4. Set up gif encoder
     * 5. Extract frames
     * 6. Convert frames to BufferedImages
     * 7. Convert BufferedImages to int[][]
     * 8. add converted images to gif encoder
     * 9. finish gifEncoder process
    **/
    @Component
    public static class TestConverter implements CommandLineRunner {
        @Override
        public void run(String... args) throws Exception {
            FFmpegFrameGrabber frameGrabber = new FFmpegFrameGrabber("/home/krzys/Projects/Projekt%20In%C5%BCynierski/in-motion-server-java/src/main/resources/static/file2.mp4");
            frameGrabber.start();
            int videoLength = frameGrabber.getLengthInVideoFrames() - 1;
            int parsedFrame = 0;
            ImageOptions imageOptions = new ImageOptions();
//            int delay = (int) (frameGrabber.getLengthInTime()*(1000/videoLength));
            int delay = 50;
            imageOptions.setDelay(delay, TimeUnit.MILLISECONDS);
            imageOptions.setDitherer(FloydSteinbergDitherer.INSTANCE);
            System.out.println("Begining gif creation");
            long startTime = System.currentTimeMillis();
            Frame frame;
            try (FileOutputStream outputStream = new FileOutputStream("/home/krzys/Projects/Projekt%20In%C5%BCynierski/in-motion-server-java/src/main/resources/static/converted_file.gif")) {
                GifEncoder gifEncoder = new GifEncoder(outputStream, frameGrabber.getImageWidth(), frameGrabber.getImageHeight(), 0);
                Java2DFrameConverter frameConverter = new Java2DFrameConverter();
                BufferedImage bufferedImage;
                while (parsedFrame < videoLength) {
                    frame = frameGrabber.grabFrame(false, true, true, false, false);
                    if(parsedFrame % 2 == 0) {
                        System.out.println("frame " + parsedFrame + " from " + videoLength);
                        bufferedImage = frameConverter.convert(frame);
                        gifEncoder.addImage(convertImageToArray(bufferedImage), imageOptions);
                    }
                    parsedFrame++;
                }
                frameGrabber.stop();
                gifEncoder.finishEncoding();
                System.out.println("Conversion time: " + ((System.currentTimeMillis() - startTime)/1000) + " seconds");
            } catch (Exception e) {
                System.out.println(e.getMessage());
            }

        }

        private int[][] convertImageToArray(BufferedImage bufferedImage) throws IOException {
            int[][] rgbArray = new int[bufferedImage.getHeight()][bufferedImage.getWidth()];
            for (int i = 0; i < bufferedImage.getHeight(); i++) {
                for (int j = 0; j < bufferedImage.getWidth(); j++) {
                    rgbArray[i][j] = bufferedImage.getRGB(j, i);
                }
            }
            return rgbArray;
        }
    }
}
