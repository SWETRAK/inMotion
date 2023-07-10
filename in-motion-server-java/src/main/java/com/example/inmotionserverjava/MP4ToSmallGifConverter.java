package com.example.inmotionserverjava;

import com.squareup.gifencoder.FloydSteinbergDitherer;
import com.squareup.gifencoder.GifEncoder;
import com.squareup.gifencoder.ImageOptions;
import org.bytedeco.javacv.FFmpegFrameGrabber;
import org.bytedeco.javacv.Frame;
import org.bytedeco.javacv.Java2DFrameConverter;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.web.multipart.MultipartFile;

import java.awt.image.BufferedImage;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.util.concurrent.TimeUnit;

import static org.bytedeco.ffmpeg.global.swscale.SWS_BICUBIC;

public class MP4ToSmallGifConverter {

    private static final int OUTPUT_IMAGE_WIDTH = 96;
    private static final int OUTPUT_IMAGE_HEIGHT = 128;
    private static final int OUTPUT_IMAGE_FRAME_QUANTITY = 75;

    private static final int DELAY = 50;

    private final MultipartFile mp4File;

    private byte[] output;

    private final Logger logger;

    private final FFmpegFrameGrabber frameGrabber;

    private final ImageOptions imageOptions;

    public MP4ToSmallGifConverter(MultipartFile mp4File) {

        this.mp4File = mp4File;
        this.output = null;
        this.logger = LoggerFactory.getLogger(this.getClass());
        this.frameGrabber = frameGrabber();
        this.imageOptions = imageOptions();
    }

    public void convert() {

        long startTime = System.currentTimeMillis();
        logger.info("Begining gif creation");
        try (ByteArrayOutputStream outputStream = new ByteArrayOutputStream()) {
            GifEncoder gifEncoder = new GifEncoder(outputStream, OUTPUT_IMAGE_WIDTH, OUTPUT_IMAGE_HEIGHT, 100);

            int[][][] videoFrames = getVideoFrames();

            for (int i = 0; i < OUTPUT_IMAGE_FRAME_QUANTITY; i++) {
                gifEncoder.addImage(videoFrames[i], imageOptions);
            }

            gifEncoder.finishEncoding();
            this.output = outputStream.toByteArray();
        } catch (IOException e) {
            logger.error(e.getMessage());
        }

        String finishMessage = String.format("Conversion time: %d seconds ", (System.currentTimeMillis() - startTime) / 1000);
        logger.info(finishMessage);
    }

    private int[][][] getVideoFrames() {
        int[][][] frames = new int[OUTPUT_IMAGE_FRAME_QUANTITY][OUTPUT_IMAGE_WIDTH][OUTPUT_IMAGE_HEIGHT];
        try (Java2DFrameConverter frameConverter = new Java2DFrameConverter()) {
            int pendingFrame = 0;
            int parsedFrame = 0;
            int skipFrames = (int) frameGrabber.getFrameRate() / 15;
            Frame frame;

            while (parsedFrame < OUTPUT_IMAGE_FRAME_QUANTITY) {
                frame = frameGrabber.grabFrame(false, true, true, false, false);
                if (pendingFrame % skipFrames == 0) {
                    frames[parsedFrame] = convertImageToArray(frameConverter.convert(frame));
                    parsedFrame++;
                }
                pendingFrame++;
            }

            frameGrabber.stop();
        } catch (FFmpegFrameGrabber.Exception e) {
            logger.error(e.getMessage());
        }

        return frames;
    }

    private FFmpegFrameGrabber frameGrabber() {
        try {
            FFmpegFrameGrabber grabber = new FFmpegFrameGrabber(mp4File.getInputStream());
            grabber.start();
            grabber.setImageWidth(96);
            grabber.setImageHeight(128);
            grabber.setImageScalingFlags(SWS_BICUBIC);
            return grabber;
        } catch (FFmpegFrameGrabber.Exception ex) {
            logger.error("Couldn't start frameGrabber");
        } catch (IOException e) {
            logger.error("Couldn't get input stream of file");
        }

        return null;
    }

    private ImageOptions imageOptions() {
        ImageOptions options = new ImageOptions();
        options.setDelay(DELAY, TimeUnit.MILLISECONDS);
        options.setDitherer(FloydSteinbergDitherer.INSTANCE);
        return options;
    }


    private int[][] convertImageToArray(BufferedImage bufferedImage) {
        int[][] rgbArray = new int[bufferedImage.getHeight()][bufferedImage.getWidth()];
        for (int i = 0; i < bufferedImage.getHeight(); i++) {
            for (int j = 0; j < bufferedImage.getWidth(); j++) {
                rgbArray[i][j] = bufferedImage.getRGB(j, i);
            }
        }
        return rgbArray;
    }

    public byte[] getOutput() {
        return output;
    }
}
