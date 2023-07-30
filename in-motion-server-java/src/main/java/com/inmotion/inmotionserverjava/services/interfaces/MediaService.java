package com.inmotion.inmotionserverjava.services.interfaces;

import com.inmotion.inmotionserverjava.model.ProfileVideoUploadInfoDto;
import org.springframework.web.multipart.MultipartFile;

public interface MediaService {
    ProfileVideoUploadInfoDto addProfileVideo(MultipartFile mp4File, String jwtToken);
    byte[] getProfileVideoAsMp4(String nickname, String userId);
    byte[] getProfileVideoAsGif(String nickname, String userId);
}
