package com.inmotion.inmotionserverjava.model.message;

import java.io.Serializable;
import java.util.List;

public record UpdatePostVideoMetadataMessage(
        String postId,
        String authorId,
        List<VideoMetadataMessage> videosMetaData
) implements Serializable {
}
