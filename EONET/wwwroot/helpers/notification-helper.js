import { notification } from 'antd';

export const openSuccessNotification = (message) => {
    notification["success"]({
      message: message,
      placement: 'bottomRight'
    });
};

export const openErrorNotification = (message) => {
    notification["error"]({
      message: message,
      placement: 'bottomRight'
    });
};