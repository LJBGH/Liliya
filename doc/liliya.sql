/*
 Navicat Premium Data Transfer

 Source Server         : MyRDS
 Source Server Type    : MySQL
 Source Server Version : 80022
 Source Host           : rm-uf61ev7xoo84r6707fo.mysql.rds.aliyuncs.com:3306
 Source Schema         : liliya

 Target Server Type    : MySQL
 Target Server Version : 80022
 File Encoding         : 65001

 Date: 11/11/2021 10:35:48
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for sys_datadictionary
-- ----------------------------
DROP TABLE IF EXISTS `sys_datadictionary`;
CREATE TABLE `sys_datadictionary`  (
  `Id` varchar(36) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '主键Id',
  `Title` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '数据字典标题',
  `Value` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '数据字典值',
  `Remark` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '数据字典备注',
  `ParentId` varchar(36) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '数据字典父级',
  `Sort` int(0) NULL DEFAULT NULL COMMENT '排序',
  `Code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '唯一编码',
  `CreatedId` varchar(36) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '创建人Id',
  `CreatedAt` datetime(0) NULL DEFAULT NULL COMMENT '创建时间',
  `LastModifyId` varchar(36) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '最后修改人Id',
  `LastModifedAt` datetime(0) NULL DEFAULT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '数据字典表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_datadictionary
-- ----------------------------
INSERT INTO `sys_datadictionary` VALUES ('55686e14-4c74-4a8c-9b46-b80862ba73b8', '测试1_1_1', 'string', 'string', 'dbff554c-4f0d-405e-9158-2f0cc13de52a', 1, 'string', '00000000-0000-0000-0000-000000000000', '2021-11-10 17:08:06', NULL, '0001-01-01 00:00:00', b'0');
INSERT INTO `sys_datadictionary` VALUES ('8a280861-d3ce-4492-9f85-02b964a66eac', '测试2_1', 'string', 'string', '9ecc7f86-bbae-4cb6-97bb-2a5c4cef655e', 1, 'string', '00000000-0000-0000-0000-000000000000', '2021-11-10 17:08:37', NULL, '0001-01-01 00:00:00', b'0');
INSERT INTO `sys_datadictionary` VALUES ('9ecc7f86-bbae-4cb6-97bb-2a5c4cef655e', '测试2', 'string', 'string', '00000000-0000-0000-0000-000000000000', 2, 'string', '00000000-0000-0000-0000-000000000000', '2021-11-10 17:08:14', NULL, '0001-01-01 00:00:00', b'0');
INSERT INTO `sys_datadictionary` VALUES ('9f5aedd1-b011-40d2-b360-4b469457e971', '测试2_2', 'string', 'string', '9ecc7f86-bbae-4cb6-97bb-2a5c4cef655e', 2, 'string', '00000000-0000-0000-0000-000000000000', '2021-11-10 17:08:42', NULL, '0001-01-01 00:00:00', b'0');
INSERT INTO `sys_datadictionary` VALUES ('d20e9e63-d008-4d58-a72a-7a5f57b23490', '测试1', 'string', 'string', '00000000-0000-0000-0000-000000000000', 1, 'string', '00000000-0000-0000-0000-000000000000', '2021-11-10 17:07:03', NULL, '0001-01-01 00:00:00', b'0');
INSERT INTO `sys_datadictionary` VALUES ('dbff554c-4f0d-405e-9158-2f0cc13de52a', '测试1_1', 'string', 'string', 'd20e9e63-d008-4d58-a72a-7a5f57b23490', 1, 'string', '00000000-0000-0000-0000-000000000000', '2021-11-10 17:07:38', NULL, '0001-01-01 00:00:00', b'0');
INSERT INTO `sys_datadictionary` VALUES ('dd5c3eb3-8fac-4870-a2cf-fc0532e95fd9', '测试2_3', 'string', 'string', '9ecc7f86-bbae-4cb6-97bb-2a5c4cef655e', 3, 'string', '00000000-0000-0000-0000-000000000000', '2021-11-10 17:08:45', NULL, '0001-01-01 00:00:00', b'0');
INSERT INTO `sys_datadictionary` VALUES ('e55b3456-c435-4664-8e8b-5a82597a3f55', '测试1_2', 'string', 'string', 'd20e9e63-d008-4d58-a72a-7a5f57b23490', 2, 'string', '00000000-0000-0000-0000-000000000000', '2021-11-10 17:07:46', NULL, '0001-01-01 00:00:00', b'0');

-- ----------------------------
-- Table structure for sys_user
-- ----------------------------
DROP TABLE IF EXISTS `sys_user`;
CREATE TABLE `sys_user`  (
  `Id` varchar(36) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '主键Id',
  `Account` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '账号',
  `Password` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '密码',
  `Name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '人员姓名',
  `JobNumber` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '工号',
  `Department` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '部门',
  `Position` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '职位',
  `CreatedId` varchar(36) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '创建人Id',
  `CreatedAt` datetime(0) NULL DEFAULT NULL COMMENT '创建时间',
  `LastModifyId` varchar(36) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '最后修改人Id',
  `LastModifedAt` datetime(0) NULL DEFAULT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '用户表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_user
-- ----------------------------
INSERT INTO `sys_user` VALUES ('85069614-0d72-4d74-af63-c9a7f5ac4195', 'lijianbiao', '123456', '李见彪', '001', '研发部', '.Net工程师', '00000000-0000-0000-0000-000000000000', '2021-11-10 15:53:51', NULL, '0001-01-01 00:00:00', b'0');
INSERT INTO `sys_user` VALUES ('db03fec2-f391-481b-9e24-32370a6a9942', 'admin', '123456', '管理员', '000', 'IT部', '网管', '00000000-0000-0000-0000-000000000000', '2021-11-10 15:52:03', NULL, '0001-01-01 00:00:00', b'0');

SET FOREIGN_KEY_CHECKS = 1;
