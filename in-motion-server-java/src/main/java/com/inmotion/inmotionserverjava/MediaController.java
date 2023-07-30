package com.inmotion.inmotionserverjava;

import com.inmotion.inmotionserverjava.model.ProfileVideoUploadInfoDto;
import com.inmotion.inmotionserverjava.services.interfaces.MediaService;
import lombok.AllArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

@RestController
@RequestMapping("/api/media")
@AllArgsConstructor
public class MediaController {

    private final MediaService mediaService;

    @PostMapping("/profile/video")
    public ResponseEntity<ProfileVideoUploadInfoDto> postProfileVideo(@RequestParam("mp4File") MultipartFile mp4File,
                                                                      @RequestHeader("authentication") String jwtToken){
        return new ResponseEntity<>(mediaService.addProfileVideo(mp4File, jwtToken), HttpStatus.OK);
    }

    @GetMapping("/profile/video/gif/{username}/{userId}")
    public ResponseEntity<byte[]> getProfileVideoAsGif(@PathVariable String username, @PathVariable String userId){
        return new ResponseEntity<>(mediaService.getProfileVideoAsGif(username, userId), HttpStatus.OK);
    }

    @GetMapping("/profile/video/mp4/{username}/{userId}")
    public ResponseEntity<byte[]> getProfileVideoAsMp4(@PathVariable String username, @PathVariable String userId){
        return new ResponseEntity<>(mediaService.getProfileVideoAsGif(username, userId), HttpStatus.OK);
    }
}
