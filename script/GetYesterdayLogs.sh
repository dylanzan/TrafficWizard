#!/bin/bash

ACCESS_LOG_FILE_PATH=""
TAGET_LOG_FILE_PATH=""
YESTERDAY_DATE=`date  +"%Y-%m-%d" -d  "-1day"`

cat ${ACCESS_LOG_FILE_PATH} |grep ${YESTERDAY_DATE} >> ${TAGET_LOG_FILE_PATH}/${YESTERDAY_DATE}
touch ${TAGET_LOG_FILE_PATH}/`date +%Y%m%d`.oks