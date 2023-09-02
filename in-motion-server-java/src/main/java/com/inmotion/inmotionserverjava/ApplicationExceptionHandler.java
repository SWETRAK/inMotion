package com.inmotion.inmotionserverjava;

import com.inmotion.inmotionserverjava.exceptions.ErrorResponse;
import com.inmotion.inmotionserverjava.exceptions.minio.MinioFileNotFoundException;
import com.inmotion.inmotionserverjava.exceptions.minio.MinioFilePostingException;
import com.inmotion.inmotionserverjava.exceptions.converter.BadFileExtensionException;
import com.inmotion.inmotionserverjava.exceptions.converter.ConversionException;
import com.inmotion.inmotionserverjava.exceptions.converter.FrameExtractionException;
import com.inmotion.inmotionserverjava.exceptions.converter.FrameGrabberInitializationException;
import lombok.extern.slf4j.Slf4j;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.multipart.MultipartException;
import org.springframework.web.servlet.mvc.method.annotation.ResponseEntityExceptionHandler;

@Slf4j
@ControllerAdvice
public class ApplicationExceptionHandler extends ResponseEntityExceptionHandler {

    @ExceptionHandler({MinioFileNotFoundException.class})
    private ResponseEntity<ErrorResponse> handleMinioFileNotFoundException(MinioFileNotFoundException e) {
        ErrorResponse error = new ErrorResponse(e.getMessage());
        log.error(error.message());
        return new ResponseEntity<>(error, HttpStatus.NOT_FOUND);
    }

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
