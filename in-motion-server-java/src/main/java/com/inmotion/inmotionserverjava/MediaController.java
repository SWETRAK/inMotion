package com.inmotion.inmotionserverjava;

import com.inmotion.inmotionserverjava.exceptions.ErrorResponse;
import com.inmotion.inmotionserverjava.model.*;
import com.inmotion.inmotionserverjava.model.message.AuthenticationMessage;
import com.inmotion.inmotionserverjava.model.message.UpdatePostVideoMetadataMessage;
import com.inmotion.inmotionserverjava.model.message.UpdateUserProfileVideoMessage;
import com.inmotion.inmotionserverjava.model.message.VideoMetadataMessage;
import com.inmotion.inmotionserverjava.services.MessagePublisher;
import com.inmotion.inmotionserverjava.services.interfaces.MediaService;
import io.swagger.v3.oas.annotations.media.Content;
import io.swagger.v3.oas.annotations.media.Schema;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.responses.ApiResponses;
import lombok.AllArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.UUID;

@RestController
@RequestMapping("/api/media")
@AllArgsConstructor
public class MediaController {

    private final MediaService mediaService;
    private final MessagePublisher messagePublisher;

    @GetMapping(value = "/test")
    public ResponseEntity test()
    {
        String postId = "b84be523-da1c-40e8-b14d-d39074aa3711";
        String authorId = "e453a7b4-722f-477c-b3b6-8065d365b88f";
//        messagePublisher.publishJwtValidationEvent(new AuthenticationMessage("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImRhNzdkNzgzLTNkYzMtNGM1ZC1iMjZhLTBhMzdlMDYzYzA2OCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImthbWlscGlldHJhazEyM0BnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiU3dldHJhayIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlVzZXIiLCJleHAiOjE2OTkxODY5NDYsImlzcyI6ImlybC1iYWNrZW5kOjgwIiwiYXVkIjoiaXJsLWJhY2tlbmQ6ODAifQ.3YplDGFHhOTu9F2QqmOuuPxOKZ6_F4LmqGj8hk58R9s"));
//        messagePublisher.publishVideoUploadedEvent(new UpdatePostVideoMetadataMessage(postId, authorId, new ArrayList<VideoMetadataMessage>(
//                Arrays.asList(
//                        new VideoMetadataMessage("Kamil", "Pietrak", "filename", "test", "Front"),
//                        new VideoMetadataMessage("Kamil", "Pietrak", "filename", "test", "Rear")
//                )
//        )));
         messagePublisher.publishUserProfileVideoUploadEvent(new UpdateUserProfileVideoMessage(UUID.randomUUID().toString(), "", "", "", ""));
        return new ResponseEntity<>(HttpStatus.OK);
    }

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
                                                                      @RequestHeader("authentication") String jwtToken) {
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
                                                     @RequestHeader("authentication") String jwtToken) {
        return new ResponseEntity<>(mediaService.addPost(frontVideo, backVideo, jwtToken), HttpStatus.CREATED);
    }

    @GetMapping(value = "/profile/video/gif/{username}/{userId}", produces = "image/gif")
    @ApiResponses(value = {
            @ApiResponse(responseCode = "200", description = "Profile video gif found",
                    content = @Content(mediaType = "image/gif", schema = @Schema(implementation = byte[].class))),
            @ApiResponse(responseCode = "404", description = "Profile video gif doesn't exist or wrong data provided",
                    content = @Content(mediaType = "application/json", schema = @Schema(implementation = byte[].class)))
    })
    public ResponseEntity<byte[]> getProfileVideoAsGif(@PathVariable String username, @PathVariable String userId) {
        return new ResponseEntity<>(mediaService.getProfileVideoAsGif(username, userId), HttpStatus.OK);
    }

    @GetMapping(value = "/profile/video/mp4/{username}/{userId}", produces = "video/mp4")
    @ApiResponses(value = {
            @ApiResponse(responseCode = "200", description = "Profile video found",
                    content = @Content(mediaType = "image/gif", schema = @Schema(implementation = byte[].class))),
            @ApiResponse(responseCode = "404", description = "Profile video doesn't exist or wrong data provided",
                    content = @Content(mediaType = "application/json", schema = @Schema(implementation = ErrorResponse.class)))
    })
    public ResponseEntity<byte[]> getProfileVideoAsMp4(@PathVariable String username, @PathVariable String userId) {
        return new ResponseEntity<>(mediaService.getProfileVideoAsMp4(username, userId), HttpStatus.OK);
    }

    @GetMapping("/post/{postId}")
    @ApiResponses(value = {
            @ApiResponse(responseCode = "200", description = "Post found",
                    content = @Content(mediaType = "image/gif", schema = @Schema(implementation = PostDto.class))),
            @ApiResponse(responseCode = "404", description = "Post not found",
                    content = @Content(mediaType = "application/json", schema = @Schema(implementation = ErrorResponse.class)))
    })
    public ResponseEntity<PostDto> getPostById(@PathVariable String postId) {
        return new ResponseEntity<>(mediaService.getPostById(postId), HttpStatus.OK);
    }
}
