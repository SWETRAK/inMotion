package com.example.inmotionserverjava;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;

@RestController
@RequestMapping("/media")
public class MediaController {

    MediaService mediaService;

    public MediaController(MediaService mediaService) {
        this.mediaService = mediaService;
    }

    @PostMapping()
    public ResponseEntity<String> postMp4File(@RequestParam("mp4File") MultipartFile mp4File){
        mediaService.addProfileGif("test_poniedziela.gif", mp4File);
        return new ResponseEntity<>("OK", HttpStatus.OK);
    }
}
