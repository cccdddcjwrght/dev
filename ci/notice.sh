link=$CI_PROJECT_URL/-/commit/$CI_COMMIT_SHA
url=$RESURL
res=$RESURL/artifacts/download
msg=$NOTICE_TEXT

msg=${msg/__gc/$CI_PROJECT_NAME}
msg=${msg/__br/$CI_COMMIT_BRANCH}
msg=${msg/__cm/$CI_COMMIT_TITLE}
msg=${msg/__cid/$CI_PIPELINE_ID}
msg=${msg/__murl/$link}
msg=${msg/__job/$CI_BUILD_NAME}

if [ $RESULT == 'Fail' ]; then
    msg=${msg/__purl/$url}
    msg=${msg/__res/<font color="#ff0000">$RESULT</font>}
else
    msg=${msg/__res/<font color="#00ffee">$RESULT</font>}
    msg=${msg/__purl/$res}
fi

curl "$CURL" -H "$CURL_HEAD" -d "$msg" 
