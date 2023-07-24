package com.inmotion.inmotionserverjava;

import lombok.AllArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;

@RestController
@RequestMapping("/api/media")
@AllArgsConstructor
public class MediaController {

    MediaService mediaService;

    @PostMapping("/profile/video")
    public ResponseEntity<String> postProfileVideo(@RequestParam("mp4File") MultipartFile mp4File,
                                              @RequestHeader("authentication") String jwtToken){
        mediaService.addProfileVideo(mp4File, jwtToken);
        return new ResponseEntity<>("OK", HttpStatus.OK);
    }
}
