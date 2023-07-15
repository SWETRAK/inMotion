package com.example.inmotionserverjava;

import lombok.AllArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;

@RestController
@RequestMapping("/api/media")
@AllArgsConstructor
public class MediaController {

    MediaService mediaService;

    @PostMapping()
    public ResponseEntity<String> postMp4File(@RequestParam("mp4File") MultipartFile mp4File, @RequestParam("nickname") String nickname){
        mediaService.addProfileVideo("profile_video", mp4File, nickname);
        return new ResponseEntity<>("OK", HttpStatus.OK);
    }
}
