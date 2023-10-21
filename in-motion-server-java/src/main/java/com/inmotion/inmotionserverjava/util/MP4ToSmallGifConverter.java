package com.inmotion.inmotionserverjava.util;

import com.inmotion.inmotionserverjava.exception.converter.BadFileExtensionException;
import com.inmotion.inmotionserverjava.exception.converter.ConversionException;
import com.inmotion.inmotionserverjava.exception.converter.FrameExtractionException;
import com.squareup.gifencoder.FloydSteinbergDitherer;
import com.squareup.gifencoder.GifEncoder;
import com.squareup.gifencoder.ImageOptions;
import lombok.NoArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.bytedeco.javacv.FFmpegFrameGrabber;
import org.bytedeco.javacv.Frame;
import org.bytedeco.javacv.Java2DFrameConverter;
import org.springframework.stereotype.Component;
import org.springframework.web.multipart.MultipartFile;

import java.awt.image.BufferedImage;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.util.Objects;
import java.util.concurrent.TimeUnit;

import static org.bytedeco.ffmpeg.global.swscale.SWS_BICUBIC;

@Slf4j
@Component
@NoArgsConstructor
public class MP4ToSmallGifConverter {

    private static final int OUTPUT_IMAGE_WIDTH = 96;
    private static final int OUTPUT_IMAGE_HEIGHT = 128;
    private static final int OUTPUT_IMAGE_FRAME_QUANTITY = 75;
    private static final int DELAY = 50;
    private MultipartFile mp4File;
    private byte[] output;
    private FFmpegFrameGrabber frameGrabber;
    private ImageOptions imageOptions;

    public byte[] convert(MultipartFile mp4File) {
        init(mp4File);

        long startTime = System.currentTimeMillis();
        log.info("Beginning gif creation");
        try (ByteArrayOutputStream outputStream = new ByteArrayOutputStream()) {
            GifEncoder gifEncoder = new GifEncoder(outputStream, OUTPUT_IMAGE_WIDTH, OUTPUT_IMAGE_HEIGHT, 100);

            int[][][] videoFrames = getVideoFrames();

            for (int i = 0; i < OUTPUT_IMAGE_FRAME_QUANTITY; i++) {
                gifEncoder.addImage(videoFrames[i], imageOptions);
            }

            gifEncoder.finishEncoding();
            this.output = outputStream.toByteArray();
        } catch (IOException e) {
            throw new ConversionException(e.getMessage());
        }

        String finishMessage = String.format("Conversion time: %d seconds ", (System.currentTimeMillis() - startTime) / 1000);
        log.info(finishMessage);

        return this.output;
    }

    private void init(MultipartFile mp4File){
        if(!Objects.requireNonNull(mp4File.getOriginalFilename()).toLowerCase().endsWith("mp4")){
            throw new BadFileExtensionException();
        }
        this.mp4File = mp4File;
        this.output = null;
        this.frameGrabber = frameGrabber();
        this.imageOptions = imageOptions();
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
            throw new FrameExtractionException(e.getMessage());
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
        } catch (IOException e) {
            throw new FrameExtractionException(e.getMessage());
        }
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
}
