package com.example.inmotionserverjava;

import com.example.inmotionserverjava.exceptions.ErrorResponse;
import com.example.inmotionserverjava.exceptions.minio.MinioFilePostingException;
import com.example.inmotionserverjava.exceptions.converter.BadFileExtensionException;
import com.example.inmotionserverjava.exceptions.converter.ConversionException;
import com.example.inmotionserverjava.exceptions.converter.FrameExtractionException;
import com.example.inmotionserverjava.exceptions.converter.FrameGrabberInitializationException;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.multipart.MultipartException;
import org.springframework.web.servlet.mvc.method.annotation.ResponseEntityExceptionHandler;

@ControllerAdvice
public class ApplicationExceptionHandler extends ResponseEntityExceptionHandler {

    private final Logger log = LoggerFactory.getLogger(ApplicationExceptionHandler.class);

    @ExceptionHandler({MultipartException.class})
    private ResponseEntity<ErrorResponse> handleMultipartException(MultipartException e){
        ErrorResponse error = new ErrorResponse(e.getMessage());
        log.error(error.message());
        return new ResponseEntity<>(error, HttpStatus.BAD_REQUEST);
    }

    @ExceptionHandler({MinioFilePostingException.class})
    private ResponseEntity<ErrorResponse> handleMinioFilePostingException(MinioFilePostingException e) {
        ErrorResponse error = new ErrorResponse(e.getMessage());
        log.error(error.message());
        return new ResponseEntity<>(error, HttpStatus.BAD_REQUEST);
    }

    @ExceptionHandler({BadFileExtensionException.class, ConversionException.class, FrameExtractionException.class,
            FrameGrabberInitializationException.class})
    private ResponseEntity<ErrorResponse> handleMp4ToGIFConverterExceptions(RuntimeException e){
        ErrorResponse error = new ErrorResponse(e.getMessage());
        log.error(error.message());
        return new ResponseEntity<>(error, HttpStatus.BAD_REQUEST);
    }
}
