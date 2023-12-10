package com.inmotion.inmotionserverjava.service.interfaces;

import com.inmotion.inmotionserverjava.model.PostUploadInfoDto;
import com.inmotion.inmotionserverjava.model.ProfileVideoUploadInfoDto;
import org.springframework.web.multipart.MultipartFile;

public interface MediaService {
    ProfileVideoUploadInfoDto addProfileVideo(MultipartFile mp4File, String jwtToken);

    PostUploadInfoDto addPost(MultipartFile frontVideo, MultipartFile backVideo, String postId, String jwtToken);

    byte[] getProfileVideoAsMp4(String userId);

    byte[] getProfileVideoAsGif(String userId);

    byte[] getPostById(String postId, String side);

}
