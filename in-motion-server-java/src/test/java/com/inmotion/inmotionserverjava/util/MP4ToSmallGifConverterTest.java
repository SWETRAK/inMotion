package com.inmotion.inmotionserverjava.util;

import com.inmotion.inmotionserverjava.exceptions.converter.BadFileExtensionException;
import org.junit.jupiter.api.Test;
import org.springframework.mock.web.MockMultipartFile;
import org.springframework.web.multipart.MultipartFile;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;

import static org.assertj.core.api.Assertions.assertThat;
import static org.assertj.core.api.Assertions.assertThatThrownBy;

class MP4ToSmallGifConverterTest {
    private static final Path INPUT_VIDEO_PATH = Paths.get("src/test/resources/converter_test_resources/input_video.mp4");
    private static final Path OUTPUT_GIF_PATH =  Paths.get("src/test/resources/converter_test_resources/output_gif.gif");
    private final MP4ToSmallGifConverter converter = new MP4ToSmallGifConverter();

    @Test
    void SuccessfullConversionTest() throws IOException {
        String inputVideoName = INPUT_VIDEO_PATH.getFileName().toString();
        String inputVideoContentType = Files.probeContentType(INPUT_VIDEO_PATH);
        String outputGifName = OUTPUT_GIF_PATH.getFileName().toString();
        MultipartFile inputVideo = new MockMultipartFile(outputGifName, inputVideoName, inputVideoContentType,
                Files.readAllBytes(INPUT_VIDEO_PATH));

        byte[] outputGif = converter.convert(inputVideo);

        assertThat(outputGif).isEqualTo(Files.readAllBytes(OUTPUT_GIF_PATH));
    }


    @Test
    void wrongFileExtensionTest() throws IOException {
        String wrongVideoFileName = OUTPUT_GIF_PATH.getFileName().toString();
        String wrongVideoFileContentType = Files.probeContentType(OUTPUT_GIF_PATH);
        MultipartFile wrongInputVideo = new MockMultipartFile(wrongVideoFileName, wrongVideoFileName, wrongVideoFileContentType,
                Files.readAllBytes(OUTPUT_GIF_PATH));

        assertThatThrownBy(() -> converter.convert(wrongInputVideo))
                .isInstanceOf(BadFileExtensionException.class)
                .hasMessageContaining("File extension is not mp4!");
    }
}
