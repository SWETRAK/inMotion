package com.inmotion.inmotionserverjava;

import com.inmotion.inmotionserverjava.exceptions.ErrorResponse;
import com.inmotion.inmotionserverjava.model.*;
import com.inmotion.inmotionserverjava.services.interfaces.MediaService;
import io.swagger.v3.oas.annotations.media.Content;
import io.swagger.v3.oas.annotations.media.Schema;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.responses.ApiResponses;
import lombok.AllArgsConstructor;
import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

import java.util.List;

@RestController
@RequestMapping("/api/media")
@AllArgsConstructor
public class MediaController {

    private final MediaService mediaService;
    private final RabbitTemplate rabbitTemplate;

    @GetMapping
    public void test() {
        rabbitTemplate.convertAndSend("validate-jwt-event", "",
                new MasstransitEvent<>(
                        //--IMS:Shared:Messaging:Messages:JWT:RequestJwtValidationMessage
                        List.of("IMS:Shared:Messaging:Messages:IMSBaseMessage", "IMS:Shared:Messaging:Messages:JWT:RequestJwtValidationMessage"),
                        new BaseMessage<>(false, null,
                                new AuthenticationMessage("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9." +
                                        "eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2Ns" +
                                        "YWltcy9uYW1laWRlbnRpZmllciI6IjFkNzI0MDZlLTE1MTYtNDZiNC04MTI2LWMwZDZmNWY1" +
                                        "NTIyNCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2" +
                                        "xhaW1zL2VtYWlsYWRkcmVzcyI6InRlc3RAdGVzdC5wbCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvY" +
                                        "XAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJ0ZXN0IiwiaHR0cDovL3Nj" +
                                        "aGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVXN" +
                                        "lciIsImV4cCI6MTY5NDM1MTE3MiwiaXNzIjoiaXJsLWJhY2tlbmQ6ODAiLCJhdWQiOiJpcmwtYmF" +
                                        "ja2VuZDo4MCJ9.QnpfAUMAg4v4WYmxu3aC6JvrbtcYihpYlRjEat63ILU")
                        )
                ));
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
