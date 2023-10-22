package com.inmotion.inmotionserverjava.service.interfaces;

import com.inmotion.inmotionserverjava.model.PostDto;
import com.inmotion.inmotionserverjava.model.PostUploadInfoDto;
import com.inmotion.inmotionserverjava.model.ProfileVideoUploadInfoDto;
import org.springframework.web.multipart.MultipartFile;

public interface MediaService {
    ProfileVideoUploadInfoDto addProfileVideo(MultipartFile mp4File, String jwtToken);

    PostUploadInfoDto addPost(MultipartFile frontVideo, MultipartFile backVideo, String jwtToken);

    byte[] getProfileVideoAsMp4(String nickname, String userId);

    byte[] getProfileVideoAsGif(String nickname, String userId);

    PostDto getPostById(String postId);

}
