// import uuid from "uuid/v4";
class Util {
  extend(...args: any[]) {
    let options,
      name,
      src,
      srcType,
      copy,
      copyType,
      copyIsArray,
      clone,
      target = args[0] || {},
      i = 1,
      length = args.length,
      deep = false;
    if (typeof target === "boolean") {
      deep = target;
      target = args[i] || {};
      i++;
    }
    if (typeof target !== "object" && typeof target !== "function") {
      target = {};
    }
    if (i === length) {
      target = this;
      i--;
    }
    for (; i < length; i++) {
      if ((options = args[i]) !== null) {
        for (name in options) {
          src = target[name];
          copy = options[name];
          if (target === copy) {
            continue;
          }
          srcType = Array.isArray(src) ? "array" : typeof src;
          if (
            deep &&
            copy &&
            ((copyIsArray = Array.isArray(copy)) || typeof copy === "object")
          ) {
            if (copyIsArray) {
              copyIsArray = false;
              clone = src && srcType === "array" ? src : [];
            } else {
              clone = src && srcType === "object" ? src : {};
            }
            target[name] = this.extend(deep, clone, copy);
          } else if (copy !== undefined) {
            target[name] = copy;
          }
        }
      }
    }
    return target;
  }
  formatDate(Data, fmt) {
    const date = new Date(Data);
    let month: string | number = date.getMonth() + 1;
    let strDate: string | number = date.getDate();
    let hours = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
    let minutes =
      date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
    let seconds =
      date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();
    if (month <= 9) {
      month = "0" + month;
    }
    if (strDate <= 9) {
      strDate = "0" + strDate;
    }
    if (fmt == "yyyy-MM-dd HH:mm:ss") {
      return (
        date.getFullYear() +
        "-" +
        month +
        "-" +
        strDate +
        " " +
        hours +
        ":" +
        minutes +
        ":" +
        seconds
      );
    } else if (fmt == "yyyy-MM-dd") {
      return date.getFullYear() + "-" + month + "-" + strDate;
    } else if (fmt == "yyyy年MM月dd日") {
      return date.getFullYear() + "年" + month + "月" + strDate + "日";
    }
    return fmt;
  }
  /*
   *   功能:实现DateAdd功能.
   *   参数:interval,字符串表达式，表示要添加的时间间隔.Y-年 q-季度 M-月 W-周 D h m s
   *   参数:number,数值表达式，表示要添加的时间间隔的个数.
   *   参数:date,时间对象.
   *   返回:新的时间对象.
   *   var now = new Date();
   *   var newDate = DateAdd( "d", 5, now);
   *---------------   DateAdd(interval,number,date)   -----------------
   */
  DateAdd(interval:string, number:number, date:Date) {
    switch (interval) {
      case "Y": {
        date.setFullYear(date.getFullYear() + number);
        return date;
        break;
      }
      case "q": {
        date.setMonth(date.getMonth() + number * 3);
        return date;
        break;
      }
      case "M": {
        date.setMonth(date.getMonth() + number);
        return date;
        break;
      }
      case "W": {
        date.setDate(date.getDate() + number * 7);
        return date;
        break;
      }
      case "D": {
        date.setDate(date.getDate() + number);
        return date;
        break;
      }
      case "h": {
        date.setHours(date.getHours() + number);
        return date;
        break;
      }
      case "m": {
        date.setMinutes(date.getMinutes() + number);
        return date;
        break;
      }
      case "s": {
        date.setSeconds(date.getSeconds() + number);
        return date;
        break;
      }
      default: {
        date.setDate(date.getDate() + number);
        return date;
        break;
      }
    }
  }
  removeEnd(str, remove) {
    return str.substr(str.length - remove.length) === remove
      ? str.substr(0, str.length - remove.length)
      : str;
  }
  joint(arr: string[], spilt: string = ",") {
    let str = "";
    arr.map((i) => (str += i + spilt));
    str = this.removeEnd(str, spilt);
    return str;
  }
  /**数组去重 */
  unique(arr) {
    let unique = {};
    arr.forEach(function(item) {
      unique[JSON.stringify(item)] = item; //键名不会重复
    });
    arr = Object.keys(unique).map(function(u) {
      //Object.keys()返回对象的所有键值组成的数组，map方法是一个遍历方法，返回遍历结果组成的数组.将unique对象的键名还原成对象数组
      return JSON.parse(u);
    });
    return arr;
  }
  /**类似与c#的Task.Delay() */
  async delay(duration: number) {
    return new Promise((resolve, reject) => {
      setTimeout(resolve, duration);
    });
  }
}
const util = new Util();
export default util;
