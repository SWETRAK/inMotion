package com.inmotion.inmotionserverjava.controller;

import com.inmotion.inmotionserverjava.exception.ErrorResponse;
import com.inmotion.inmotionserverjava.model.*;
import com.inmotion.inmotionserverjava.service.interfaces.MediaService;
import io.swagger.v3.oas.annotations.media.Content;
import io.swagger.v3.oas.annotations.media.Schema;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.responses.ApiResponses;
import io.swagger.v3.oas.annotations.tags.Tag;
import lombok.AllArgsConstructor;
import org.springframework.http.HttpHeaders;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

@CrossOrigin
@RestController
@RequestMapping("/api")
@AllArgsConstructor
@Tag(name = "Media", description = "Requests for uploading profile videos, and posts, as well as retrieving them.")
public class MediaController {

    private final MediaService mediaService;

    @PostMapping(value = "/profile/video", consumes = "multipart/form-data")
    @ApiResponses(value = {
            @ApiResponse(responseCode = "201", description = "Profile video uploaded",
                    content = @Content(mediaType = "application/json", schema = @Schema(implementation = ProfileVideoUploadInfoDto.class))),
            @ApiResponse(responseCode = "400", description = "Error during upload",
                    content = @Content(mediaType = "application/json", schema = @Schema(implementation = ErrorResponse.class))),
            @ApiResponse(responseCode = "403", description = "User is not logged in therefore he cannot post profile video",
                    content = @Content(mediaType = "application/json", schema = @Schema(implementation = ErrorResponse.class)))
    })
    public ResponseEntity<ProfileVideoUploadInfoDto> postProfileVideo(@RequestParam("mp4File") MultipartFile mp4File,
                                                                      @RequestHeader("Authorization") String jwtToken) {
        return new ResponseEntity<>(mediaService.addProfileVideo(mp4File, jwtToken), HttpStatus.CREATED);
    }

    @PostMapping(value = "/post", consumes = "multipart/form-data")
    @ApiResponses(value = {
            @ApiResponse(responseCode = "201", description = "Post uploaded",
                    content = @Content(mediaType = "application/json", schema = @Schema(implementation = PostUploadInfoDto.class))),
            @ApiResponse(responseCode = "400", description = "Error during upload",
                    content = @Content(mediaType = "application/json", schema = @Schema(implementation = ErrorResponse.class))),
            @ApiResponse(responseCode = "403", description = "User is not logged in therefore he cannot post",
                    content = @Content(mediaType = "application/json", schema = @Schema(implementation = ErrorResponse.class)))
    })
    public ResponseEntity<PostUploadInfoDto> addPost(@RequestParam("frontVideo") MultipartFile frontVideo,
                                                     @RequestParam("backVideo") MultipartFile backVideo,
                                                     @RequestParam("postID") String postId,
                                                     @RequestHeader("Authorization") String jwtToken) {
        return new ResponseEntity<>(mediaService.addPost(frontVideo, backVideo, postId, jwtToken), HttpStatus.CREATED);
    }

    @GetMapping(value = "/profile/video/gif/{userId}", produces = "image/gif")
    @ApiResponses(value = {
            @ApiResponse(responseCode = "200", description = "Profile video gif found",
                    content = @Content(mediaType = "image/gif", schema = @Schema(implementation = byte[].class))),
            @ApiResponse(responseCode = "404", description = "Profile video gif doesn't exist or wrong data provided",
                    content = @Content(mediaType = "application/json", schema = @Schema(implementation = byte[].class)))
    })
    public ResponseEntity<byte[]> getProfileVideoAsGif(@PathVariable String userId) {
        return new ResponseEntity<>(mediaService.getProfileVideoAsGif(userId), HttpStatus.OK);
    }

    @GetMapping(value = "/profile/video/mp4/{userId}", produces = "video/mp4")
    @ApiResponses(value = {
            @ApiResponse(responseCode = "200", description = "Profile video found",
                    content = @Content(mediaType = "video/mp4", schema = @Schema(implementation = byte[].class))),
            @ApiResponse(responseCode = "404", description = "Profile video doesn't exist or wrong data provided",
                    content = @Content(mediaType = "application/json", schema = @Schema(implementation = ErrorResponse.class)))
    })
    public ResponseEntity<byte[]> getProfileVideoAsMp4(@PathVariable String userId) {
        return new ResponseEntity<>(mediaService.getProfileVideoAsMp4(userId), HttpStatus.OK);
    }

    @GetMapping("/post/{postId}/side/{side}")
    @ApiResponses(value = {
            @ApiResponse(responseCode = "200", description = "Post found",
                    content = @Content(mediaType = "video/mp4", schema = @Schema(implementation = byte[].class))),
            @ApiResponse(responseCode = "404", description = "Post not found",
                    content = @Content(mediaType = "application/json", schema = @Schema(implementation = ErrorResponse.class)))
    })
    public ResponseEntity<byte[]> getSideOfPostById(@PathVariable String postId, @PathVariable("side") String side) {
        HttpHeaders headers = new HttpHeaders();
        headers.setContentType(MediaType.parseMediaType("video/mp4"));
        return new ResponseEntity<>(mediaService.getPostById(postId, side), headers, HttpStatus.OK);
    }
}
