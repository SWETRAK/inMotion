package com.inmotion.inmotionserverjava;

import com.inmotion.inmotionserverjava.exception.ErrorResponse;
import com.inmotion.inmotionserverjava.exception.UnauthorizedUserException;
import com.inmotion.inmotionserverjava.exception.minio.MinioFileNotFoundException;
import com.inmotion.inmotionserverjava.exception.minio.MinioFilePostingException;
import com.inmotion.inmotionserverjava.exception.converter.BadFileExtensionException;
import com.inmotion.inmotionserverjava.exception.converter.ConversionException;
import com.inmotion.inmotionserverjava.exception.converter.FrameExtractionException;
import com.inmotion.inmotionserverjava.exception.converter.FrameGrabberInitializationException;
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

    @ExceptionHandler({UnauthorizedUserException.class})
    private ResponseEntity<ErrorResponse> handleUnauthorizedUserException(UnauthorizedUserException e) {
        e.printStackTrace();
        ErrorResponse error = new ErrorResponse(e.getMessage());
        log.error(error.message());
        return new ResponseEntity<>(error, HttpStatus.UNAUTHORIZED);
    }

    @ExceptionHandler({MinioFileNotFoundException.class})
    private ResponseEntity<ErrorResponse> handleMinioFileNotFoundException(MinioFileNotFoundException e) {
        e.printStackTrace();
        ErrorResponse error = new ErrorResponse(e.getMessage());
        log.error(error.message());
        return new ResponseEntity<>(error, HttpStatus.NOT_FOUND);
    }

    @ExceptionHandler({MultipartException.class})
    private ResponseEntity<ErrorResponse> handleMultipartException(MultipartException e) {
        e.printStackTrace();
        ErrorResponse error = new ErrorResponse(e.getMessage());
        log.error(error.message());
        return new ResponseEntity<>(error, HttpStatus.BAD_REQUEST);
    }

    @ExceptionHandler({MinioFilePostingException.class})
    private ResponseEntity<ErrorResponse> handleMinioFilePostingException(MinioFilePostingException e) {
        e.printStackTrace();
        ErrorResponse error = new ErrorResponse(e.getMessage());
        log.error(error.message());
        return new ResponseEntity<>(error, HttpStatus.BAD_REQUEST);
    }

    @ExceptionHandler({BadFileExtensionException.class, ConversionException.class, FrameExtractionException.class,
            FrameGrabberInitializationException.class})
    private ResponseEntity<ErrorResponse> handleMp4ToGIFConverterExceptions(RuntimeException e) {
        e.printStackTrace();
        ErrorResponse error = new ErrorResponse(e.getMessage());
        log.error(error.message());
        return new ResponseEntity<>(error, HttpStatus.BAD_REQUEST);
    }
}
